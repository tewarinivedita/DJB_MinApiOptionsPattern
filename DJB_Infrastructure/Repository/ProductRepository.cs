using DJB_Core.Entities;
using DJB_Core.Interfaces;
using DJB_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DJB_Infrastructure.Repository
{
    public class ProductRepository(DataBaseContext dbContext) : IProductRepository
    {
        public async Task<IEnumerable<ProductEntity>> GetProducts() 
        { 
            return await dbContext.Products.ToListAsync();
        }

        public async Task<ProductEntity> GetProductsAsync(Guid id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductEntity> AddProductAsync(ProductEntity entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductEntity> UpdateProductAsync(Guid id,ProductEntity entity)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                product.Name = entity.Name;
                await dbContext.SaveChangesAsync();
                return product;
            }
            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                
                dbContext.Products.Remove(product);
                
                return await dbContext.SaveChangesAsync()>0;
            }
            return false;
        }

    }
}
