
using Microsoft.Extensions.Logging;

namespace DJB_Api.Middleware;

public sealed class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-ID";

    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(
        RequestDelegate next,
        ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Reuse a valid GUID supplied by the caller.
        // Generate a new ID if the header is missing or invalid.
        var correlationId = GetCorrelationId(context);

        context.TraceIdentifier = correlationId;

        // Include the correlation ID in logs created in this scope.
        using (_logger.BeginScope(
            new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
        {
            // Return the correlation ID to the client.
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[HeaderName] = correlationId;
                return Task.CompletedTask;
            });

            _logger.LogInformation(
                "Incoming request: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception processing request");

                throw;
            }
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        var incomingId = context.Request.Headers[HeaderName]
            .FirstOrDefault();

        return Guid.TryParse(incomingId, out var parsedId)
            ? parsedId.ToString()
            : Guid.NewGuid().ToString();
    }
}
