

using DJB_Core.DataTransferObjects;
using DJB_Core.Entities;
using DJB_Core.Models;

namespace DJB_Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetOrders();
        Task<OrderEntity> GetOrderAsync(Guid id);
        Task<IEnumerable<MostOrderedProductDto>> GetMostOrderedProductsAsync(AnalyticsRequest request, CancellationToken cancellationToken);
        Task<List<OrderEntity>> GetOrdersAsync(OrderFilter filter, CancellationToken cancellationToken);
        Task<OrderEntity> AddOrderAsync(OrderEntity entity);
        Task<OrderEntity> UpdateOrderAsync(Guid id, OrderEntity entity);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}