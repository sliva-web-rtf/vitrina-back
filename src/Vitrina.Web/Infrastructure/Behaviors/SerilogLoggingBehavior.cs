using System.Diagnostics;
using MediatR;
using Serilog.Context;


namespace Vitrina.Web.Infrastructure.Behaviors;

public class SerilogLoggingBehavior<TRequest, TResponse>(
    ILogger<SerilogLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        using (LogContext.PushProperty("RequestName", requestName))
        {
            logger.LogInformation(
                "[MediatR] Starting request {RequestName}, input: {@RequestJson}",
                requestName, request);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();
                stopwatch.Stop();
                logger.LogInformation(
                    "[MediatR] Completed request {RequestName} in {ElapsedMs}ms, output: {@ResponseJson}",
                    requestName, stopwatch.ElapsedMilliseconds, response);

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
