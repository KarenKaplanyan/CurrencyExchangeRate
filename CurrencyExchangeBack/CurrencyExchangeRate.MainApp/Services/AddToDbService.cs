using AutoMapper;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Repositories;
using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Services;

public class AddToDbService: IAddToDbService
{
    
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;
    private readonly IMapper _mapper;

    public AddToDbService(
        ICurrencyExchangeRateRepository currencyExchangeRateRepository,
        IMapper mapper)
    {
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
        _mapper = mapper;
    }

    public async Task AddDataToDbAsync(ValCurs cursValues, CancellationToken cancellationToken)
    {
        if (!cursValues.Valute.Any())
        {
            return;
        }
        
        var currencies = _mapper.Map<IEnumerable<Currency>>(cursValues.Valute);
        
        await _currencyExchangeRateRepository.AddOrUpdateRangeAsync(currencies, cancellationToken);
    }
}