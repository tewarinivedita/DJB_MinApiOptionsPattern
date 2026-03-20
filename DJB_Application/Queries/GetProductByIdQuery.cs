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
    public record GetProductByIdQuery(Guid Product_id) : IRequest<ProductEntity>;

    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductEntity>
    {
        public async Task<ProductEntity> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetProductsAsync(request.Product_id);
        }
    }
}
