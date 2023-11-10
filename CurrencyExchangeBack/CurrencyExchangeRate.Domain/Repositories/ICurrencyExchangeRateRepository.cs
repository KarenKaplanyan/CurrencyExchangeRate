using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Infrastructure;

namespace CurrencyExchangeRate.Domain.Repositories;

public interface ICurrencyExchangeRateRepository
{
    Task<int> AddOrUpdateRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken);
    Task<IEnumerable<Currency>> GetCurrenciesWithPagingAsync(PagingParameters parameters, CancellationToken cancellationToken);
    Task<Currency?> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
}