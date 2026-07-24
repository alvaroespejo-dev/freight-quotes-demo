using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Shared.Dtos.Constant;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Constants.Get;

public class GetConstantsHandler(IConstantRepository constants, IMapper mapper) : IRequestHandler<GetConstantsQuery, GetConstantsResponse>
{
    private readonly IConstantRepository _constants = constants;
    private readonly IMapper _mapper = mapper;

    public async Task<GetConstantsResponse> Handle(GetConstantsQuery request, CancellationToken ct)
    {
        var all = await _constants.GetByConstantTypeIdsAsync(request.ConstantTypeIds, ct);
        var dtos = _mapper.Map<List<ConstantResponse>>(all);
        return new GetConstantsResponse(dtos);
    }
}
