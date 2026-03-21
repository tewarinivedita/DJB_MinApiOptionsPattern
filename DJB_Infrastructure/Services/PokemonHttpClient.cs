using DJB_Core.Models;
using System.Net.Http.Json;

namespace DJB_Infrastructure.Services
{
    public class PokemonHttpClient(HttpClient httpClient) : IPokemonHttpClient
    {
        public async Task<PokeMonData> GetData()
        {
            return await httpClient.GetFromJsonAsync<PokeMonData>("pokemon");
        }
    }
}
