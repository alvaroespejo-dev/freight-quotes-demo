using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Country;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class CountryMapper : Profile
{
    public CountryMapper()
    {
        CreateMap<Country, CountryResponse>().ReverseMap();
    }
}