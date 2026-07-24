using AEspejo.FreightQuotes.Api.Features.Carriers.Create;
using AEspejo.FreightQuotes.Api.Features.Carriers.Delete;
using AEspejo.FreightQuotes.Api.Features.Carriers.Get;
using AEspejo.FreightQuotes.Api.Features.Carriers.Update;
using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.Carriers;

[ApiController]
[Route("api/[controller]")]
public class CarriersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetCarriersResponse>> Get(CancellationToken ct)
        => Ok(await _mediator.Send(new GetCarriersQuery(), ct));

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] SaveCarrierRequest request, CancellationToken ct)
    {
        var carrierId = await _mediator.Send(new CreateCarrierCommand(request), ct);
        return Ok(carrierId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] SaveCarrierRequest request, CancellationToken ct)
    {
        var updated = await _mediator.Send(new UpdateCarrierCommand(id, request), ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteCarrierCommand(id), ct);
        return NoContent();
    }
}
