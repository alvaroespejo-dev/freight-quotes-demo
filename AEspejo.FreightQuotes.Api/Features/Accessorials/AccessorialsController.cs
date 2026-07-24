using AEspejo.FreightQuotes.Api.Features.Accessorials.Get;
using AEspejo.FreightQuotes.Api.Features.Accessorials.Update;
using AEspejo.FreightQuotes.Shared.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials;

[ApiController]
[Route("api/[controller]")]
public class AccessorialsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetAccessorialsResponse>> GetAll(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAccessorialsQuery(), ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateName(long id, [FromBody] UpdateNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Name is required.");
        }

        var updated = await _mediator.Send(new UpdateAccessorialNameCommand(id, request.Name), ct);
        return updated ? NoContent() : NotFound();
    }
}