using AEspejo.FreightQuotes.Api.Hubs;
using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace AEspejo.FreightQuotes.Api.Features.RateQuotes.Get
{
    public class GetRateQuotesHandler(IHubContext<RateQuoteHub> hub, ICarrierRepository carriers, IRateQuoteService rateQuoteService, IRateQuoteResolverService rateQuoteResolver) : IRequestHandler<GetRateQuotesQuery, GetRateQuotesResponse>
    {
        private readonly IHubContext<RateQuoteHub> _hub = hub;
        private readonly ICarrierRepository _carriers = carriers;
        private readonly IRateQuoteService _rateQuoteService = rateQuoteService;
        private readonly IRateQuoteResolverService _rateQuoteResolver = rateQuoteResolver;

        public async Task<GetRateQuotesResponse> Handle(GetRateQuotesQuery rateQuote, CancellationToken ct)
        {
            string requestId = rateQuote.rateQuoteRequest.RequestId;

            // Resolve id-only fields into the codes real carrier APIs need. Carrier-independent, so run once.
            await _rateQuoteResolver.ResolveAsync(rateQuote.rateQuoteRequest, ct);

            var carriers = await _carriers.GetByIsActiveAsync(isActive: true, ct);

            var tasks = carriers.Select(async carrier =>
            {
                try
                {
                    var quotes = await _rateQuoteService.GetQuotesAsync(rateQuote.rateQuoteRequest, carrier);
                    await _hub.Clients.Group(requestId)
                    .SendAsync("ReceiveQuotes", quotes, ct);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    await _hub.Clients.Group(requestId)
                    .SendAsync("ReceiveQuoteError", new { Carrier = carrier, Error = ex.Message }, ct);
                }
            });

            await Task.WhenAll(tasks);

            await _hub.Clients.Group(requestId).SendAsync("AllQuotesCompleted", requestId, ct);

            return null;
        }
    }
}
