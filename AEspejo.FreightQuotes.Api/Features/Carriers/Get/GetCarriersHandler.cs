using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Get;

public class GetCarriersHandler(ICarrierRepository carriers, IMapper mapper) : IRequestHandler<GetCarriersQuery, GetCarriersResponse>
{
    private readonly ICarrierRepository _carriers = carriers;
    private readonly IMapper _mapper = mapper;

    public async Task<GetCarriersResponse> Handle(GetCarriersQuery request, CancellationToken ct)
    {
        var all = await _carriers.GetAsync(ct);
        var dtos = _mapper.Map<List<CarrierResponse>>(all);
        return new GetCarriersResponse(dtos);
    }
}
