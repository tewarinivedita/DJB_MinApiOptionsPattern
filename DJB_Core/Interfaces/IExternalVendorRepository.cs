using DJB_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Core.Interfaces
{
    public interface IExternalVendorRepository
    {
        Task<ExchangeRateData> GetData();
    }
}
