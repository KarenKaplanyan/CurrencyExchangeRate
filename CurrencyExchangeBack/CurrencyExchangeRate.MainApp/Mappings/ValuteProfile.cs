using AutoMapper;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Mappings;

public class ValuteProfile : Profile
{
    public ValuteProfile()
    {
        CreateMap<Valute, Currency>()
            .ForMember(d => d.Rate, o => 
                o.MapFrom(s => Convert.ToDecimal(s.VunitRate)));
    }
}