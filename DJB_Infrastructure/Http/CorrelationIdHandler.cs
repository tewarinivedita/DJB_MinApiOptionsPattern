
using Microsoft.AspNetCore.Http;

namespace DJB_Infrastructure.Http;

public sealed class CorrelationIdHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var correlationId = _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["X-Correlation-ID"]
            .FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            request.Headers.Remove("X-Correlation-ID");
            request.Headers.TryAddWithoutValidation(
                "X-Correlation-ID",
                correlationId);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
