using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Shared.Dtos.State;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Api.Features.States.Get;

public class GetStatesHandler(IStateRepository states, IMapper mapper) : IRequestHandler<GetStatesQuery, GetStatesResponse>
{
    private readonly IStateRepository _states = states;
    private readonly IMapper _mapper = mapper;

    public async Task<GetStatesResponse> Handle(GetStatesQuery request, CancellationToken ct)
    {
        var query = _states.AsQueryable();

        if (request.CountryId.HasValue)
            query = query.Where(s => s.CountryId == request.CountryId.Value);

        var states = await query.ToListAsync(ct);
        var stateDtos = _mapper.Map<IReadOnlyList<StateResponse>>(states);

        return new GetStatesResponse(stateDtos);
    }
}