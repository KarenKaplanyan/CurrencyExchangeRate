using CurrencyExchangeRate.MainApp.Domain.Entities;

namespace CurrencyExchangeRate.MainApp.Infrastructure.Repositories.Interfaces;

public interface ICurrencyExchangeRateRepository
{
    Task<int> AddDataToDbAsync(CancellationToken cancellationToken);
}