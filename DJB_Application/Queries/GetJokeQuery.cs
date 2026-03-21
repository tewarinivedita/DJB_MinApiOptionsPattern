using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetJokeQuery() : IRequest<JokeData>;
    public class GetJokeQueryHandler(IExternalVendorRepository externalVendorRepository)
        : IRequestHandler<GetJokeQuery, JokeData>
    {
        public async Task<JokeData> Handle(GetJokeQuery request, CancellationToken cancellationToken)
        {
            return await externalVendorRepository.GetJokeData();
        }
    }
}
