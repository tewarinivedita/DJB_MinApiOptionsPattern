using DJB_Core.Models;

namespace DJB_Application.Interface
{
    public interface IAnalyticsService
    {
        Task<object> ExecuteAsync(AnalyticsRequest request, CancellationToken cancellationToken);
    }
}
