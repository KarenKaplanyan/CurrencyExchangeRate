using CurrencyExchangeRate.MainApp.Infrastructure.Dto;

namespace CurrencyExchangeRate.MainApp.Infrastructure.Services;

public interface IXmlService
{
    Task<ValCurs> GetDataFromCbr(CancellationToken cancellationToken);
}