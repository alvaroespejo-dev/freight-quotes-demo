using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Accessorial;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class AccessorialMapper : Profile
{
    public AccessorialMapper()
    {
        CreateMap<Accessorial, AccessorialResponse>().ReverseMap();
    }
}