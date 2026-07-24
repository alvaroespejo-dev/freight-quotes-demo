using AEspejo.FreightQuotes.Api.Features.States.Get;
using AEspejo.FreightQuotes.Api.Features.States.Update;
using AEspejo.FreightQuotes.Shared.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.States;

[ApiController]
[Route("api/[controller]")]
public class StatesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetStatesResponse>> GetAll([FromQuery] long? countryId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetStatesQuery(countryId), ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateName(long id, [FromBody] UpdateNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Name is required.");
        }

        var updated = await _mediator.Send(new UpdateStateNameCommand(id, request.Name), ct);
        return updated ? NoContent() : NotFound();
    }
}