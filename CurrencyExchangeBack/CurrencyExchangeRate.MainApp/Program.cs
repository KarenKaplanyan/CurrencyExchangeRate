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
    .AddAutoMapper(typeof(Program))
    .AddScoped<IXmlService, XmlService>()
    .AddScoped<IAddToDbService, AddToDbService>()
    .BuildServiceProvider();

var xmlService = serviceProvider.GetRequiredService<IXmlService>();
var addToDbService = serviceProvider.GetRequiredService<IAddToDbService>();

var cursValues = await xmlService.GetDataFromCbr(CancellationToken.None);
await addToDbService.AddDataToDbAsync(cursValues, CancellationToken.None);

Console.WriteLine("Окончание обновления справочника валют....");
Console.ReadKey();
