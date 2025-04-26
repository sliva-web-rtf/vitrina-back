using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using ValidationException = Saritasa.Tools.Domain.Exceptions.ValidationException;

namespace Vitrina.Web.Infrastructure.Middlewares;

/// <summary>
/// Exception handling middleware. In general:
/// ValidationException => 400 with additional "errors" property.
/// DomainException => 400.
/// _ => 500 with stack trace.
/// </summary>
internal sealed class ApiExceptionMiddleware
{
    public const string ErrorsKey = "errors";
    public const string CodeKey = "code";
    private const string ProblemJsonMimeType = @"application/problem+json";

    private readonly RequestDelegate next;
    private readonly IJsonHelper jsonHelper;
    private readonly ILogger<ApiExceptionMiddleware> logger;
    private readonly IWebHostEnvironment environment;
    private readonly HtmlTestEncoder encoder = new();

    private static readonly IDictionary<Type, int> ExceptionStatusCodes = new Dictionary<Type, int>
    {
        [typeof(NotFoundException)] = StatusCodes.Status404NotFound,
        [typeof(NotImplementedException)] = StatusCodes.Status501NotImplemented,
        [typeof(ForbiddenException)] = StatusCodes.Status403Forbidden,
        [typeof(UnauthorizedException)] = StatusCodes.Status401Unauthorized,
        [typeof(DomainException)] = StatusCodes.Status400BadRequest,
        [typeof(ValidationException)] = StatusCodes.Status400BadRequest,
        [typeof(InvalidOrderFieldException)] = StatusCodes.Status400BadRequest,
        [typeof(ArgumentException)] = StatusCodes.Status400BadRequest
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiExceptionMiddleware" /> class.
    /// </summary>
    public ApiExceptionMiddleware(
        RequestDelegate next,
        IJsonHelper jsonHelper,
        ILogger<ApiExceptionMiddleware> logger,
        IWebHostEnvironment environment)
    {
        this.next = next;
        this.jsonHelper = jsonHelper;
        this.logger = logger;
        this.environment = environment;
    }

    /// <summary>
    /// Invokes the next middleware.
    /// </summary>
    /// <param name="httpContext">HTTP context.</param>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            if (httpContext.Response.HasStarted)
            {
                logger.LogWarning(
                    "The response has already started, the API exception middleware will not be executed.");
                throw;
            }
            var problemDetails = GetObjectByException(exception, httpContext.RequestServices);
            problemDetails.Instance = httpContext.Request.Path;
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = ProblemJsonMimeType;

            await using var stringWriter = new StringWriter(new StringBuilder(200));
            jsonHelper.Serialize(problemDetails).WriteTo(stringWriter, encoder);
            await httpContext.Response.WriteAsync(stringWriter.ToString());
        }
    }

    private ProblemDetails GetObjectByException(Exception exception, IServiceProvider requestServices)
    {
        // Prepare model.
        var problem = new ProblemDetails();
        var statusCode = StatusCodes.Status400BadRequest;
        problem.Extensions[ErrorsKey] = Enumerable.Empty<ProblemFieldDto>();
        switch (exception)
        {
            case ValidationException validationException:
                var jsonOptions = requestServices.GetRequiredService<IOptions<JsonOptions>>();
                problem.Extensions[ErrorsKey] = validationException.Errors
                    .SelectMany(error =>
                    {
                        var field = FormatPropertyPath(error.Key, jsonOptions.Value.JsonSerializerOptions);
                        return error.Value.Select(message => new ProblemFieldDto(field, message));
                    });
                AddExceptionInfoToProblemDetails(problem, validationException);
                break;

            case DomainException domainException:
                AddExceptionInfoToProblemDetails(problem, domainException);
                statusCode = GetStatusCodeByExceptionType(domainException.GetType());
                break;

            default:
                problem.Title = "Internal server error.";
                if (exception is InvalidOrderFieldException || exception is InfrastructureException)
                {
                    problem.Title = exception.Message;
                }
                if (!environment.IsProduction())
                {
                    // Since System.Text.Json cannot serialize exception we do that partially for debug.
                    problem.Extensions["debug_exception"] =
                        new Dictionary<string, string>
                        {
                            ["Type"] = exception.GetType().ToString(),
                            ["Message"] = exception.Message,
                            ["StackTrace"] = exception.StackTrace ?? string.Empty
                        };
                }
                statusCode = GetStatusCodeByExceptionType(exception.GetType());
                logger.LogError(exception, exception.Message);
                break;
        }
        problem.Status = statusCode;
        return problem;
    }

    private static void AddExceptionInfoToProblemDetails(ProblemDetails problemDetails, DomainException exception)
    {
        problemDetails.Title = exception.Message;
        problemDetails.Type = exception.GetType().Name;
        AddCodeToProblemDetails(problemDetails, exception.Code);
    }

    private static void AddCodeToProblemDetails(ProblemDetails problemDetails, string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            problemDetails.Extensions[CodeKey] = code;
        }
    }

    private static int GetStatusCodeByExceptionType(Type exceptionType)
    {
        // Most probable case.
        if (exceptionType == typeof(DomainException))
        {
            return StatusCodes.Status400BadRequest;
        }
        foreach ((Type exceptionTypeKey, int statusCode) in ExceptionStatusCodes)
        {
            if (exceptionTypeKey.IsAssignableFrom(exceptionType))
            {
                return statusCode;
            }
        }
        return StatusCodes.Status500InternalServerError;
    }

    /// <summary>
    /// Format a path that defines a property in object to follow the JSON naming rules.
    /// </summary>
    /// <param name="propertyPath">Path to the property.</param>
    /// <param name="jsonSerializerOptions">Json options.</param>
    /// <returns>Formatted property path.</returns>
    public static string FormatPropertyPath(string propertyPath, JsonSerializerOptions jsonSerializerOptions)
    {
        if (string.IsNullOrEmpty(propertyPath) || jsonSerializerOptions.PropertyNamingPolicy == null)
        {
            return propertyPath;
        }

        var parts = propertyPath.Split('.')
            .Select(jsonSerializerOptions.PropertyNamingPolicy.ConvertName);
        return string.Join(".", parts);
    }
}
