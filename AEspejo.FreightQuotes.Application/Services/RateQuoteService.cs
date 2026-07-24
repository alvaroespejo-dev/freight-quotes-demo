using AEspejo.FreightQuotes.Application.Interfaces.Services;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;
using Microsoft.Extensions.DependencyInjection;

namespace AEspejo.FreightQuotes.Application.Services
{
    public class RateQuoteService(IServiceProvider serviceProvider) : IRateQuoteService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<IReadOnlyList<RateQuoteResponse>> GetQuotesAsync(RateQuoteRequest rateQuoteRequest, Carrier carrier)
        {
            var key = carrier.IsMockMode
                ? CarrierScacConstant.MOCK
                : (carrier.Scac ?? string.Empty).ToUpperInvariant();

            var client = _serviceProvider.GetKeyedService<ICarrierRateClient>(key)
                ?? throw new NotSupportedException($"Carrier {carrier.Scac} Not Supported.");

            var quotes = await client.GetQuoteAsync(rateQuoteRequest, carrier, CancellationToken.None);

            return quotes;
        }
    }
}
