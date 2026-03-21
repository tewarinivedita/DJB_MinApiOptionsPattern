using DJB_Core.Models;

namespace DJB_Infrastructure.Services
{
    public interface IOfficialJokeHttpClient
    {
        Task<JokeData> GetData();
    }
}