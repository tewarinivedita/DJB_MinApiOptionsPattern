using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderEntity>>;
    public class GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderEntity>>
    {
        public async Task<IEnumerable<OrderEntity>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await orderRepository.GetOrders();
        }
    }
}
