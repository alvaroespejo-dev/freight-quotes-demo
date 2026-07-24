using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Get;

public record GetCarrierSettingsQuery(long CarrierId) : IRequest<GetCarrierSettingsResponse>;
