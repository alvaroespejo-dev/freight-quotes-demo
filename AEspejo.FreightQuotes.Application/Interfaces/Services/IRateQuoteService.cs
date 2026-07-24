using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.Application.Interfaces.Services
{
    public interface IRateQuoteService
    {
        Task<IReadOnlyList<RateQuoteResponse>> GetQuotesAsync(RateQuoteRequest rateQuoteRequest, Carrier carrier);
    }
}
