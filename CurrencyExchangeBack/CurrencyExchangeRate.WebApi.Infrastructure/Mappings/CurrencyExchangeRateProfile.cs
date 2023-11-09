using AutoMapper;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Domain.Entities;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Infrastructure.Mappings;

public class CurrencyExchangeRateProfile: Profile
{
    public CurrencyExchangeRateProfile()
    {
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Currency, CurrencySuggestDto>();
    }
}