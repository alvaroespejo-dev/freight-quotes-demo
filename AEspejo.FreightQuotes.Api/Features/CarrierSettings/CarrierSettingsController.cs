using AEspejo.FreightQuotes.Api.Features.CarrierSettings.Create;
using AEspejo.FreightQuotes.Api.Features.CarrierSettings.Delete;
using AEspejo.FreightQuotes.Api.Features.CarrierSettings.Get;
using AEspejo.FreightQuotes.Api.Features.CarrierSettings.Update;
using AEspejo.FreightQuotes.Application.Exceptions;
using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings;

[ApiController]
[Route("api/[controller]")]
public class CarrierSettingsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<GetCarrierSettingsResponse>> Get([FromQuery] long carrierId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetCarrierSettingsQuery(carrierId), ct));

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] SaveCarrierSettingRequest request, CancellationToken ct)
    {
        try
        {
            var carrierSettingId = await _mediator.Send(new CreateCarrierSettingCommand(request), ct);
            return Ok(carrierSettingId);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] SaveCarrierSettingRequest request, CancellationToken ct)
    {
        var updated = await _mediator.Send(new UpdateCarrierSettingCommand(id, request), ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteCarrierSettingCommand(id), ct);
        return NoContent();
    }
}
