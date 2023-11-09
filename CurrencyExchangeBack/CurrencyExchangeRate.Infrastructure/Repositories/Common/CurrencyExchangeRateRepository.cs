using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Repositories.Interfaces;
using CurrencyExchangeRate.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace CurrencyExchangeRate.Infrastructure.Repositories.Common;

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
    
    public async Task<IEnumerable<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken )
    {
        return await _currencyDbContext.Currencies
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken)
    {
        await _currencyDbContext.AddRangeAsync(currencies, cancellationToken);
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
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }
}