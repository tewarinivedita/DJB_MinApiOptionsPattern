

namespace DJB_Core.DataTransferObjects
{
    public class MostOrderedProductDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public int TotalOrders { get; set; }

    }
}
