using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record DeleteProductCommand(Guid ProductId) : IRequest<bool>;
    public class DeleteProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.DeleteProductAsync(request.ProductId);
        }
    }
}
