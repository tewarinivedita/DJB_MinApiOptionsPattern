using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetPokemonQuery() : IRequest<PokeMonData>;
    public class GetPokemonQueryHandler(IExternalVendorRepository externalVendorRepository)
        : IRequestHandler<GetPokemonQuery, PokeMonData>
    {
        public async Task<PokeMonData> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {
            return await externalVendorRepository.GetPokemonData();
        }
    }
}
