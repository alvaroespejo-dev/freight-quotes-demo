using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Shared.Dtos.Accessorial;
using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials.Get;

public class GetAccessorialsHandler(IAccessorialRepository accessorials, IMapper mapper) : IRequestHandler<GetAccessorialsQuery, GetAccessorialsResponse>
{
    private readonly IAccessorialRepository _accessorials = accessorials;
    private readonly IMapper _mapper = mapper;

    public async Task<GetAccessorialsResponse> Handle(GetAccessorialsQuery request, CancellationToken ct)
    {
        var all = await _accessorials.GetAllAsync(ct);
        var dtos = _mapper.Map<List<AccessorialResponse>>(all);
        return new GetAccessorialsResponse(dtos);
    }
}