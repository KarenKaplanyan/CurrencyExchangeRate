using CurrencyExchangeRate.MainApp.Domain.Entities;
using CurrencyExchangeRate.MainApp.Infrastructure.Contexts;
using CurrencyExchangeRate.MainApp.Infrastructure.Repositories.Interfaces;
using CurrencyExchangeRate.MainApp.Infrastructure.Services;

namespace CurrencyExchangeRate.MainApp.Infrastructure.Repositories.Common;

public class CurrencyExchangeRateRepository: ICurrencyExchangeRateRepository
{
    private readonly CurrencyDbContext _currencyDbContext;
    private readonly IXmlService _xmlService;

    public CurrencyExchangeRateRepository(
        CurrencyDbContext currencyDbContext,
        IXmlService xmlService)
    {
        _currencyDbContext = currencyDbContext;
        _xmlService = xmlService;
    }

    public async Task<int> AddDataToDbAsync(CancellationToken cancellationToken)
    {
        var valCurs = await _xmlService.GetDataFromCbr(cancellationToken);
        foreach (var valute in valCurs.Valute)
        {
            var existCurrency = _currencyDbContext.Currencies.FirstOrDefault(c => c.Id == valute.ID 
                && c.Rate == Convert.ToDecimal(valute.VunitRate));
            if (existCurrency != null)
            {
                existCurrency.Rate = Convert.ToDecimal(valute.VunitRate);
            }
            else
            {
                var currency = new Currency
                {
                    Id = valute.ID,
                    NumCode = valute.NumCode,
                    CharCode = valute.CharCode,
                    Name = valute.Name,
                    Rate = Convert.ToDecimal(valute.VunitRate)
                };
                _currencyDbContext.Add(currency);
            }
        }
        return await _currencyDbContext.SaveChangesAsync(cancellationToken);
    }
}