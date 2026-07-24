using AEspejo.FreightQuotes.Shared.Dtos.Accessorial;
using static AEspejo.FreightQuotes.Api.Features.Accessorials.Get.GetAccessorialsResponse;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials.Get;

public record GetAccessorialsResponse(IReadOnlyList<AccessorialResponse> Accessorials);
