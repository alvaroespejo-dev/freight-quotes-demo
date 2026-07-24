using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Delete;

public class DeleteCarrierSettingHandler(ICarrierSettingRepository carrierSettings) : IRequestHandler<DeleteCarrierSettingCommand>
{
    private readonly ICarrierSettingRepository _carrierSettings = carrierSettings;

    public async Task Handle(DeleteCarrierSettingCommand request, CancellationToken cancellationToken)
    {
        await _carrierSettings.DeleteCarrierSettingAsync(request.CarrierSettingId, cancellationToken);
    }
}
