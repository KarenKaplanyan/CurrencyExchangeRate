using AutoMapper;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Mappings;

public class CurrencyExchangeRateProfile: Profile
{
    public CurrencyExchangeRateProfile()
    {
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Currency, CurrencySuggestDto>();
    }
}