using System.Diagnostics;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Context;


namespace Vitrina.Web.Infrastructure.Behaviors;

public class SerilogLoggingBehavior<TRequest, TResponse>(
    ILogger<SerilogLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        MaxDepth = 2
    };

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestJson = JsonConvert.SerializeObject(request, JsonSettings);
        using (LogContext.PushProperty("RequestName", requestName))
        {
            logger.LogInformation(
                "[MediatR] Starting request {RequestName}, input: {RequestJson}",
                requestName, requestJson);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();
                stopwatch.Stop();
                var responseJson = JsonConvert.SerializeObject(response, JsonSettings);
                logger.LogInformation(
                    "[MediatR] Completed request {RequestName} in {ElapsedMs}ms, output: {ResponseJson}",
                    requestName, stopwatch.ElapsedMilliseconds, responseJson);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();


                logger.LogError(
                    ex,
                    "[MediatR] Request {RequestName} failed after {ElapsedMs}ms",
                    requestName, stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
