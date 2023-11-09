using CurrencyExchangeRate.MainApp.Infrastructure.Contexts;
using CurrencyExchangeRate.MainApp.Infrastructure.Repositories.Common;
using CurrencyExchangeRate.MainApp.Infrastructure.Repositories.Interfaces;
using CurrencyExchangeRate.MainApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Начало обновления справочника валют....");

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddDbContext<CurrencyDbContext>(options => 
        options.UseMySQL(config.GetConnectionString("CurrencyExchangeRateDatabase")))
    .AddSingleton<IXmlService, XmlService>()
    .AddSingleton<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>()
    .BuildServiceProvider();

var currencyRepository = serviceProvider.GetService<ICurrencyExchangeRateRepository>();
if (currencyRepository != null) await currencyRepository.AddDataToDbAsync(CancellationToken.None);

Console.WriteLine("Окончание обновления справочника валют....");
Console.ReadKey();
