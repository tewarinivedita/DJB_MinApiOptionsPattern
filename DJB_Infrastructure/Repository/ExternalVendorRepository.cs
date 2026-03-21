using DJB_Core.Interfaces;
using DJB_Core.Models;
using DJB_Infrastructure.Services;

namespace DJB_Infrastructure.Repository
{
    public class ExternalVendorRepository(IOfficialJokeHttpClient officialJokeHttpClient, 
        IPokemonHttpClient pokemonHttpClient) : IExternalVendorRepository
    {   
        public async Task<JokeData> GetJokeData()
        {
            return await officialJokeHttpClient.GetData();
        }

        public async Task<PokeMonData> GetPokemonData()
        {
            return await pokemonHttpClient.GetData();
        }
    }
}
