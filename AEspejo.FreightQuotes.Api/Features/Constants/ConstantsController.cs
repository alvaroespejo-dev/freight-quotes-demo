using AEspejo.FreightQuotes.Api.Features.Constants.Get;
using AEspejo.FreightQuotes.Api.Features.Constants.Update;
using AEspejo.FreightQuotes.Shared.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.Constants;

[ApiController]
[Route("api/[controller]")]
public class ConstantsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetConstantsResponse>> GetAll([FromQuery] IReadOnlyList<long> constantTypeIds, CancellationToken ct)
        => Ok(await _mediator.Send(new GetConstantsQuery(constantTypeIds), ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateName(long id, [FromBody] UpdateNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Name is required.");
        }

        var updated = await _mediator.Send(new UpdateConstantNameCommand(id, request.Name), ct);
        return updated ? NoContent() : NotFound();
    }
}

