

using DJB_Core.Entities;
using DJB_Core.Models;

namespace DJB_Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<ProductEntity> GetProductsAsync(Guid id);
        Task<List<ProductEntity>> GetProductsAsyncAI(ProductFilter filter, CancellationToken cancellationToken);
        Task<ProductEntity> AddProductAsync(ProductEntity entity);
        Task<ProductEntity> UpdateProductAsync(Guid id, ProductEntity entity);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
