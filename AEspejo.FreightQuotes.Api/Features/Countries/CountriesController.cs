using AEspejo.FreightQuotes.Api.Features.Countries.Get;
using AEspejo.FreightQuotes.Api.Features.Countries.Update;
using AEspejo.FreightQuotes.Shared.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.Countries;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetCountriesResponse>> GetAll(CancellationToken ct)
        => Ok(await _mediator.Send(new GetCountriesQuery(), ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateName(long id, [FromBody] UpdateNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Name is required.");
        }

        var updated = await _mediator.Send(new UpdateCountryNameCommand(id, request.Name), ct);
        return updated ? NoContent() : NotFound();
    }
}