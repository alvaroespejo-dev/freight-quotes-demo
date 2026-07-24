using AEspejo.FreightQuotes.Api.Features.Accessorials.Get;
using AEspejo.FreightQuotes.Api.Features.RateQuotes.Get;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.RateQuotes
{
    [ApiController]
    [Route("api/[controller]")]
    public class RateQuoteController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<GetRateQuotesResponse>> Rate([FromBody] RateQuoteRequest request, CancellationToken ct)
        {
            _ = await _mediator.Send(new GetRateQuotesQuery(request), ct);
            return Accepted(new { request.RequestId, Status = "Fetching quotes..." });
        }
    }
}
