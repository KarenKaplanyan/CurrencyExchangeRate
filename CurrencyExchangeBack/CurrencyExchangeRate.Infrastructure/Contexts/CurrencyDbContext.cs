using CurrencyExchangeRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.Infrastructure.Contexts;

public class CurrencyDbContext: DbContext
{
    public DbSet<Currency> Currencies { get; set; }

    public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currencies");
            entity.HasKey("Id");
        });
    }
}