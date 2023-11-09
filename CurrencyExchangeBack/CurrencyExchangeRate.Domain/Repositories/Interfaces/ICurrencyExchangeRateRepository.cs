using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Infrastructure;

namespace CurrencyExchangeRate.Domain.Repositories.Interfaces;

public interface ICurrencyExchangeRateRepository
{
    Task<int> AddRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken);
    Task<IEnumerable<Currency>> GetCurrenciesWithPagingAsync(PagingParameters parameters, CancellationToken cancellationToken);
    Task<IEnumerable<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken);
    Task<Currency?> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
}