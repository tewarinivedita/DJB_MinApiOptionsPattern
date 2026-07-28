using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record class AddOrderCommand(OrderEntity order): IRequest<OrderEntity>;
    public class AddOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<AddOrderCommand, OrderEntity>
    {
        public async Task<OrderEntity> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
           return await orderRepository.AddOrderAsync(request.order);
        }
    }
}
