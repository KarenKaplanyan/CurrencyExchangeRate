using CurrencyExchangeRate.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRate.WebApi.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CurrencyExchangeRateDatabase");
        services.AddDbContext<CurrencyDbContext>(options =>
            options.UseMySQL(connectionString));
        return services;
    }
}