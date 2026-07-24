using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Delete;

public record DeleteCarrierSettingCommand(long CarrierSettingId) : IRequest;
