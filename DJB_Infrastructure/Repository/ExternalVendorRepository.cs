using DJB_Core.Interfaces;
using DJB_Core.Models;
using DJB_Infrastructure.Services;

namespace DJB_Infrastructure.Repository
{
    public class ExternalVendorRepository(OfficialJokeHttpClient officialJokeHttpClient): IExternalVendorRepository
    {
        public async Task<ExchangeRateData> GetData()
        {
            return await officialJokeHttpClient.GetData();
        }

    }
}
