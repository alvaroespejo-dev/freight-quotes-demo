using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.CarrierApiClient.Interfaces
{
    public interface ICarrierRateClient
    {
        Task<IReadOnlyList<RateQuoteResponse>> GetQuoteAsync(RateQuoteRequest freightQuote, Carrier carrier, CancellationToken ct);
    }
}
