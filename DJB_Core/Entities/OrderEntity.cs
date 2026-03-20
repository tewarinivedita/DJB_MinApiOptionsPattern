namespace DJB_Core.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string Date { get; set; } = null!;
        public string TotalPurchase { get; set; } = null!;
        public List<int> productids { get; set; } = null!;

    }
}
