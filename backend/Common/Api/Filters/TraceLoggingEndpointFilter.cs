using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.Api.Filters;
public class TraceLoggingEndpointFilter(ILogger<TraceLoggingEndpointFilter> logger) : IEndpointFilter
{

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var activityName = $"{httpContext.Request.Method} {httpContext.Request.Path}";
        using var activity = new Activity(activityName);
        activity.Start();
        activity.AddEvent(new ActivityEvent($"Request started at {DateTime.UtcNow}"));
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var result = await next(context);
            return result;
        }
        finally
        {
            stopwatch.Stop();
            activity.AddEvent(new ActivityEvent($"Request finished in {stopwatch.ElapsedMilliseconds} ms"));

            logger.LogInformation("{Method} {Path} completed in {ElapsedMilliseconds} ms",
                httpContext.Request.Method,
                httpContext.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}

public static class TraceLoggingEndpointFilterExtension
{
    public static RouteHandlerBuilder AddTelemetryFilter(this RouteHandlerBuilder builder)
        => builder.AddEndpointFilter<TraceLoggingEndpointFilter>();
}
