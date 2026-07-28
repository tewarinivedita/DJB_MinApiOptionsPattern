
namespace DJB_Core.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string LongDescription { get; set; }

        public string ShortDescription { get; set; }
        public ImagesViewModel ProfileImg { get; set; }

        public ImagesViewModel ThumbnailImg { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int StockCount { get; set; }


        public bool IsStockAvailable { get; set; }
        public string Dose { get; set; }
        public List<string> KeyBenefits { get; set; }

    }
}
