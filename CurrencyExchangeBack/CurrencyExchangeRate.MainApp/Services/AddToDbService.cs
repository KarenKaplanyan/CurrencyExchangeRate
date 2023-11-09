using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Repositories.Interfaces;
using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Services;

public class AddToDbService: IAddToDbService
{
    
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;

    public AddToDbService(ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
    }

    public async Task AddDataToDbAsync(ValCurs cursValues, CancellationToken cancellationToken)
    {
        if (!cursValues.Valute.Any())
        {
            return;
        }
        
        var existCurrencies = new List<Currency>();
        
        var currencies = await _currencyExchangeRateRepository
            .GetCurrenciesAsync(cancellationToken);
        foreach (var cursValue in cursValues.Valute)
        {
            var existCurrency = currencies
                .FirstOrDefault(c => c.Id == cursValue.ID 
                && c.Rate == Convert.ToDecimal(cursValue.VunitRate));
            if (existCurrency != null)
            {
                existCurrency.Rate = Convert.ToDecimal(cursValue.VunitRate);
            }
            else
            {
                var currency = new Currency
                {
                    Id = cursValue.ID,
                    Name = cursValue.Name,
                    Rate = Convert.ToDecimal(cursValue.VunitRate)
                };
                existCurrencies.Add(currency);
            }
        }
        await _currencyExchangeRateRepository.AddRangeAsync(existCurrencies, cancellationToken);
    }
}