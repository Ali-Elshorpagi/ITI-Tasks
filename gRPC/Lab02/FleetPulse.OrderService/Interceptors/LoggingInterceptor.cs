using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FleetPulse.OrderService.Advanced.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var watch = Stopwatch.StartNew();

        _logger.LogInformation("================================");
        _logger.LogInformation("Method  : {Method}", context.Method);
        _logger.LogInformation("Started : {StartedAt}", DateTime.Now);

        var response = await continuation(request, context);

        watch.Stop();

        _logger.LogInformation("Finished : {FinishedAt}", DateTime.Now);
        _logger.LogInformation("Duration : {ElapsedMs} ms", watch.ElapsedMilliseconds);
        _logger.LogInformation("================================");

        return response;
    }
}