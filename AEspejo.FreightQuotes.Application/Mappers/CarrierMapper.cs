using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class CarrierMapper : Profile
{
    public CarrierMapper()
    {
        CreateMap<Carrier, CarrierResponse>().ReverseMap();
    }
}
