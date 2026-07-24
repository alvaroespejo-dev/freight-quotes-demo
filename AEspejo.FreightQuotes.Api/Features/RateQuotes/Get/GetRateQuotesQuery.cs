using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.RateQuotes.Get
{
    public record GetRateQuotesQuery(RateQuoteRequest rateQuoteRequest) : IRequest<GetRateQuotesResponse>;
}
