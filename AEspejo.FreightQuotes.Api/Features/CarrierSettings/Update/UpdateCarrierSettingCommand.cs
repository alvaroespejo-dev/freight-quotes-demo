using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Update;

public record UpdateCarrierSettingCommand(long CarrierSettingId, SaveCarrierSettingRequest CarrierSetting) : IRequest<bool>;
