using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJB_Core.Entities;
using DJB_Core.Interfaces;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductEntity>>;
    public class GetAllProductsQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductEntity>>
    {
        public async Task<IEnumerable<ProductEntity>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetProducts();
        }
    }
}
