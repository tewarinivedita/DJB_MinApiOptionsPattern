using DJB_Core.Entities;
using DJB_Core.Interfaces;
using DJB_Core.Models;
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

        public async Task<List<ProductEntity>> GetProductsAsyncAI(ProductFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<ProductEntity> query = dbContext.Products;

            if (!string.IsNullOrWhiteSpace(filter.ProductName))
            {
                query = query.Where(x => x.Name == filter.ProductName);
            }

            if (filter.Price.HasValue)
            {
                query = query.Where(x => x.Price == filter.Price);
            }

            if (filter.StockAvailable.HasValue)
            {
                query = query.Where(x => x.StockAvailable == filter.StockAvailable);
            }

            return await query.ToListAsync(cancellationToken);
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
