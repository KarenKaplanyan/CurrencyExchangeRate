using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Interfaces;

public interface ICurrencyExchangeRateRepository
{
    Task<IEnumerable<CurrencySuggestDto>> GetCurrenciesAsync(PagingParameters parameters, CancellationToken cancellationToken);
    Task<CurrencyDto> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
}