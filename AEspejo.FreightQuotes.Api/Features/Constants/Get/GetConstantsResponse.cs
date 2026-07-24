using AEspejo.FreightQuotes.Shared.Dtos.Constant;
using static AEspejo.FreightQuotes.Api.Features.Constants.Get.GetConstantsResponse;

namespace AEspejo.FreightQuotes.Api.Features.Constants.Get;

public record GetConstantsResponse(IReadOnlyList<ConstantResponse> Constants)
{
    
}
