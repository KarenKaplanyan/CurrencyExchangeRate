using AutoMapper;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Dto;

namespace CurrencyExchangeRate.WebApi.Mappings;

public class PagingDtoProfile: Profile
{
    public PagingDtoProfile()
    {
        CreateMap<PagingDto, PagingParameters>();
    }
}