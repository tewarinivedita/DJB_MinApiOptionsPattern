using DJB_Core.Models;
using System.Net.Http.Json;

namespace DJB_Infrastructure.Services
{
    public class OfficialJokeHttpClient(HttpClient httpClient) : IOfficialJokeHttpClient
    {
        public async Task<JokeData> GetData()
        {
            return await httpClient.GetFromJsonAsync<JokeData>("random_joke");
        }
    }
}
