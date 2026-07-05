using System.Diagnostics;

namespace gift_shop.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        var requestTime = DateTime.UtcNow;

        _logger.LogInformation($"[REQUEST] {requestMethod} {requestPath} - Started at {requestTime:yyyy-MM-dd HH:mm:ss.fff}");

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;
            var responseTime = stopwatch.ElapsedMilliseconds;

            var logLevel = statusCode >= 500 ? LogLevel.Error :
                          statusCode >= 400 ? LogLevel.Warning :
                          LogLevel.Information;

            _logger.Log(logLevel, 
                $"[RESPONSE] {requestMethod} {requestPath} - Status: {statusCode} - Duration: {responseTime}ms - Ended at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}");
        }
    }
}

public static class RequestLoggingExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
