using DJB_Core.Models;

namespace DJB_Infrastructure.Services
{
    public interface IPokemonHttpClient
    {
        Task<PokeMonData> GetData();
    }
}