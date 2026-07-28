using DJB_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DJB_Infrastructure.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
