using AutoMapper;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Mappings;

public class CurrencyDtoProfile: Profile
{
    public CurrencyDtoProfile()
    {
        CreateMap<Currency, CurrencyDto>();
    }
}