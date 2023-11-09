namespace CurrencyExchangeRate.Domain.Entities;

public class Currency
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Rate { get; set; }
}