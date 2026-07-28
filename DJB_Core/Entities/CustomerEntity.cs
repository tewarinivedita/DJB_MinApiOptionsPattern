using System.ComponentModel.DataAnnotations;

namespace DJB_Core.Entities
{
    public class CustomerEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

    }
}
