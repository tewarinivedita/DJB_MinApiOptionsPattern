using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Commands
{
    public record class AddProductCommand(ProductEntity product): IRequest<ProductEntity>;
    public class AddProductCommandHandler(IProductRepository productRepository) : IRequestHandler<AddProductCommand, ProductEntity>
    {
        public async Task<ProductEntity> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
           return await productRepository.AddProductAsync(request.product);
        }
    }
}
