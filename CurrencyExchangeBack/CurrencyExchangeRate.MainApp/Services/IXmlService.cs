using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Services;

public interface IXmlService
{
    Task<ValCurs> GetDataFromCbr(CancellationToken cancellationToken);
}