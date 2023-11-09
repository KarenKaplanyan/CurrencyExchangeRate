using CurrencyExchangeRate.Domain.Repositories.Interfaces;
using CurrencyExchangeRate.Infrastructure.Repositories.Common;
using CurrencyExchangeRate.MainApp.Services;
using CurrencyExchangeRate.WebApi.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Начало обновления справочника валют....");

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddDb(config)
    .AddSingleton<IXmlService, XmlService>()
    .AddSingleton<IAddToDbService, AddToDbService>()
    .AddSingleton<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>()
    .BuildServiceProvider();

var xmlService = serviceProvider.GetService<IXmlService>();
var addToDbService = serviceProvider.GetService<IAddToDbService>();

if (xmlService != null && addToDbService != null)
{
    var cursValues = await xmlService.GetDataFromCbr(CancellationToken.None);
    await addToDbService.AddDataToDbAsync(cursValues, CancellationToken.None);
}



Console.WriteLine("Окончание обновления справочника валют....");
Console.ReadKey();
