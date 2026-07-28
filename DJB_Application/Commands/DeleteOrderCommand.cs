using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record DeleteOrderCommand(Guid OrderId) : IRequest<bool>;
    public class DeleteOrderCommandHandler(IOrderRepository orderRepository)
        : IRequestHandler<DeleteOrderCommand, bool>
    {
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await orderRepository.DeleteOrderAsync(request.OrderId);
        }
    }
}
