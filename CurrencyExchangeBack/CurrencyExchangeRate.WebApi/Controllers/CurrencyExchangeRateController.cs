using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;
using CurrencyExchangeRate.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.WebApi.Controllers;

[ApiController]
[Authorize(Roles = "user,admin")]
[Route("[controller]")]
public class CurrencyExchangeRateController
{
    private readonly ICurrencyExchangeRateService _currencyExchangeRateService;

    public CurrencyExchangeRateController(ICurrencyExchangeRateService currencyExchangeRateService)
    {
        _currencyExchangeRateService = currencyExchangeRateService;
    }
    
    
    [HttpGet("currencies")]
    public async Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync(
        [FromQuery] int pageIndex,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateService.GetCurrenciesWithPagingAsync(new PagingDto
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        }, cancellationToken);
    }
    
    [HttpGet("currency")]
    public async Task<ActionResult<CurrencyDto>> GetCurrencyById([FromQuery] string currencyId, CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateService.GetCurrencyAsync(currencyId, cancellationToken);
    }
    
    [HttpGet("totalCount")]
    public async Task<int> GetCurrencyTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateService.GetTotalCountAsync(cancellationToken);
    }

}