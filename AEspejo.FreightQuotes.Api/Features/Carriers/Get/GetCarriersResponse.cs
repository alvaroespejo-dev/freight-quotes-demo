using AEspejo.FreightQuotes.Shared.Dtos.Carrier;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Get;

public record GetCarriersResponse(IReadOnlyList<CarrierResponse> Carriers);
