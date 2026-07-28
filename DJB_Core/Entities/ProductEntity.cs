using System.ComponentModel.DataAnnotations;

namespace DJB_Core.Entities
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
    }
}
