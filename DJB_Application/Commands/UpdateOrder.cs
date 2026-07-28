using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record UpdateOrderCommand(Guid OrderId, OrderEntity Order) : IRequest<OrderEntity>;
    public class UpdateOrder(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderCommand, OrderEntity>
    {
        public async Task<OrderEntity> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await orderRepository.UpdateOrderAsync(request.OrderId, request.Order    );
        }
    }
}
