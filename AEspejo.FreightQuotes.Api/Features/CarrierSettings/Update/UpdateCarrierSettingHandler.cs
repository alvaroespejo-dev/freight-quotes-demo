using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Update;

public class UpdateCarrierSettingHandler(ICarrierSettingRepository carrierSettings, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCarrierSettingCommand, bool>
{
    private readonly ICarrierSettingRepository _carrierSettings = carrierSettings;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateCarrierSettingCommand request, CancellationToken cancellationToken)
    {
        var carrierSetting = await _carrierSettings.GetByIdAsync(request.CarrierSettingId, cancellationToken);
        if (carrierSetting is null)
        {
            return false;
        }

        carrierSetting.CarrierId = request.CarrierSetting.CarrierId;
        carrierSetting.SettingTypeId = request.CarrierSetting.SettingTypeId;
        carrierSetting.CarrierSettingTypeId = request.CarrierSetting.CarrierSettingTypeId;
        carrierSetting.Value = request.CarrierSetting.Value;
        carrierSetting.IsActive = request.CarrierSetting.IsActive;

        await _carrierSettings.UpdateAsync(carrierSetting, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
