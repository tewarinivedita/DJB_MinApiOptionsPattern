using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetExchangeRateQuery() : IRequest<ExchangeRateData>;
    public class GetExchangeRateQueryHandler(IExternalVendorRepository externalVendorRepository)
        : IRequestHandler<GetExchangeRateQuery, ExchangeRateData>
    {
        public async Task<ExchangeRateData> Handle(GetExchangeRateQuery request, CancellationToken cancellationToken)
        {
            return await externalVendorRepository.GetData();
        }
    }
}
