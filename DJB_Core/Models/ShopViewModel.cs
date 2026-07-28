using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Core.Models
{
    public class ShopViewModel
    {
        public List<string> GeneralText { get; set; }
        public List<ProductViewModel> Products { get; set; } 
        public string AddToCartButtonText { get; set; }
        public string BuyNowButtonText { get; set; }
    }
}
