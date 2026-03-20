

using DJB_Core.Entities;

namespace DJB_Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<ProductEntity> GetProductsAsync(Guid id);
        Task<ProductEntity> AddProductAsync(ProductEntity entity);
        Task<ProductEntity> UpdateProductAsync(Guid id, ProductEntity entity);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
