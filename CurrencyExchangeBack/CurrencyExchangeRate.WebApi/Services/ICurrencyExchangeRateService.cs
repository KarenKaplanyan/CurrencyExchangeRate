using CurrencyExchangeRate.WebApi.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Services;

public interface ICurrencyExchangeRateService
{
    Task<IEnumerable<CurrencyDto>> GetCurrenciesWithPagingAsync(PagingDto paging, CancellationToken cancellationToken);
    Task<CurrencyDto> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
}