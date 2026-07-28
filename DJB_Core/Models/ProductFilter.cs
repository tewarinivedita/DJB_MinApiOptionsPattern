using DJB_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Core.Models
{

    public class ProductFilter
    {
        public string? ProductName { get; set; }

        public bool? IsStockAvailable { get; set; }
        public decimal? Price { get; set; }
        public decimal? StockAvailable { get; set; }
    }
}
