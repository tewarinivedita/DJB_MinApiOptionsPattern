using System.ComponentModel.DataAnnotations;

namespace DJB_Core.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        public string Date { get; set; } = null!;
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }

    }
}
