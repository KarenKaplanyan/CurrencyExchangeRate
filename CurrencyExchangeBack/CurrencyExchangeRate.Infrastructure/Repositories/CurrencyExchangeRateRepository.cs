using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Repositories;
using CurrencyExchangeRate.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Repositories;

public class CurrencyExchangeRateRepository: ICurrencyExchangeRateRepository
{
    
    private readonly CurrencyDbContext _currencyDbContext;

    public CurrencyExchangeRateRepository(CurrencyDbContext currencyDbContext)
    {
        _currencyDbContext = currencyDbContext;
    }
    
    public async Task<IEnumerable<Currency>> GetCurrenciesWithPagingAsync(PagingParameters parameters, CancellationToken cancellationToken )
    {
        return await _currencyDbContext.Currencies
            .AsNoTracking()
            .Skip(parameters.PageIndex * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<int> AddOrUpdateRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken)
    {
        var existCurrencies = (await GetCurrenciesAsync(cancellationToken)).ToList();
        var newСurrencies = new List<Currency>();
        var updatedCurrencies = new List<Currency>();
        
        foreach (var currency in currencies)
        {
            var existCurrency = existCurrencies
                .FirstOrDefault(c => c.Id == currency.Id);
            if (existCurrency != null)
            {
                existCurrency.Rate = (decimal) 33.00;//currency.Rate;
                updatedCurrencies.Add(existCurrency);
            }
            else
            {
                var newCurrency = new Currency
                {
                    Id = currency.Id,
                    Name = currency.Name,
                    Rate = currency.Rate
                };
                newСurrencies.Add(newCurrency);
            }
        }

        _currencyDbContext.UpdateRange(updatedCurrencies);
       await _currencyDbContext.AddRangeAsync(newСurrencies, cancellationToken);
       return await _currencyDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Currency?> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken)
    {
        return await _currencyDbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _currencyDbContext.Currencies
            .CountAsync(cancellationToken);
    }
    
    private async Task<IEnumerable<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken )
    {
        return await _currencyDbContext.Currencies
            .ToListAsync(cancellationToken);
    }
}