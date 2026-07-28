using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Application.Queries
{
    public record GetOrdersByIdQuery(Guid Order_id) : IRequest<OrderEntity>;

    public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersByIdQuery, OrderEntity>
    {
        public async Task<OrderEntity> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
        {
            return await orderRepository.GetOrderAsync(request.Order_id);
        }
    }
}
