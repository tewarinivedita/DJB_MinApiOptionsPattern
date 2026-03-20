using DJB_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Infrastructure.Services
{
    public class OfficialJokeHttpClient(HttpClient httpClient)
    {
        public async Task<ExchangeRateData> GetData()
        {
            return await httpClient.GetFromJsonAsync<ExchangeRateData>("random_joke");
        }
    }
}
