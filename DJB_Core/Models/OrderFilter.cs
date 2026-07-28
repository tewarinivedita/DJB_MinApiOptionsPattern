
namespace DJB_Core.Models
{
    public class AnalyticsRequest
    {
        public AnalyticsIntent Intent { get; set; }
        public string Entity { get; set; } = "";
        public int Top { get; set; } = 10;
        public Dictionary<string, object> Parameters { get; set; } = new();
        public string Answer { get; set; }
    }

    public enum AnalyticsIntent
    {
        MostOrdered,
        LeastOrdered,
        TopCustomers,
        HighestRevenue,
        BestSellingCategory,
        MonthlySales,
        LowStock,
        TopSuppliers
    }

    public class OrderFilter
    {
        public string? DateRange { get; set; }

        public string? ProductName { get; set; }

        public bool? IsMostOrdered { get; set; }
    }
}
