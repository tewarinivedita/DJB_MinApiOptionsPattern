using DJB_Core.DataTransferObjects;
using DJB_Core.Entities;
using DJB_Core.Interfaces;
using DJB_Core.Models;
using DJB_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DJB_Infrastructure.Repository
{
    public class OrderRepository(DataBaseContext dbContext) : IOrderRepository
    {
        public async Task<IEnumerable<OrderEntity>> GetOrders() 
        { 
            return await dbContext.Orders.ToListAsync();
        }
       public async Task<IEnumerable<MostOrderedProductDto>> GetMostOrderedProductsAsync(AnalyticsRequest request, CancellationToken cancellationToken)
        {

            return await dbContext.Orders
                .GroupBy(x => new
                {
                    x.OrderId,
                    x.ProductID,
                    x.ProductName
                })
                .Select(g => new MostOrderedProductDto
                {
                    ProductId = g.Key.ProductID,
                    ProductName = g.Key.ProductName,
                    TotalOrders = g.Count()
                })
                .OrderByDescending(x => x.TotalOrders)
                .Take(request.Top)
                .ToListAsync(cancellationToken);
        }

        public async Task<OrderEntity> GetOrderAsync(Guid id)
        {
            return await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<List<OrderEntity>> GetOrdersAsync(OrderFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<OrderEntity> query = dbContext.Orders;

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<OrderEntity> AddOrderAsync(OrderEntity entity)
        {
            entity.OrderId = Guid.NewGuid();
            dbContext.Orders.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<OrderEntity> UpdateOrderAsync(Guid id, OrderEntity entity)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                order.ProductName = entity.ProductName;
                await dbContext.SaveChangesAsync();
                return order;
            }
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                dbContext.Orders.Remove(order);   
                return await dbContext.SaveChangesAsync()>0;
            }
            return false;
        }
    }
}
