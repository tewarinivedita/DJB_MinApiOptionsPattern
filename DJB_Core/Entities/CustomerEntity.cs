namespace DJB_Core.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

    }
}
