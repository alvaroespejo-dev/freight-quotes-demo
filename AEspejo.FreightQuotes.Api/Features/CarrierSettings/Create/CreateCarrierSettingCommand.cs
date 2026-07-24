using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Create;

public record CreateCarrierSettingCommand(SaveCarrierSettingRequest CarrierSetting) : IRequest<long>;
