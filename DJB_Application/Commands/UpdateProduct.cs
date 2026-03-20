using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record UpdateProductCommand(Guid ProductId, ProductEntity Product) : IRequest<ProductEntity>;
    public class UpdateProduct(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, ProductEntity>
    {
        public async Task<ProductEntity> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.UpdateProductAsync(request.ProductId, request.Product);
        }
    }
}
