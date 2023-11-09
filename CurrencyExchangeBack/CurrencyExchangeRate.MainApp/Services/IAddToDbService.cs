using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Services;

public interface IAddToDbService
{
    Task AddDataToDbAsync(ValCurs cursValues, CancellationToken cancellationToken);
}