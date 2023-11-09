namespace CurrencyExchangeRate.WebApi.Domain.Entities;

public class Currency
{
    public string Id { get; set; }
    public string NumCode { get; set; }
    public string CharCode { get; set; }
    public string Name { get; set; }
    public decimal Rate { get; set; }
}