using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;
using CurrencyExchangeRate.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyExchangeRateController
{
    private readonly ICurrencyExchangeRateService _currencyExchangeRateService;

    public CurrencyExchangeRateController(ICurrencyExchangeRateService currencyExchangeRateService)
    {
        _currencyExchangeRateService = currencyExchangeRateService;
    }
    
    [Authorize(Roles = "user,admin")]
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
    
    [Authorize(Roles = "user, admin")]
    [HttpGet("currency")]
    public async Task<ActionResult<CurrencyDto>> GetCurrencyById([FromQuery] string currencyId, CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateService.GetCurrencyAsync(currencyId, cancellationToken);
    }

    [Authorize(Roles = "user, admin")]
    [HttpGet("totalCount")]
    public async Task<int> GetCurrencyTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateService.GetTotalCountAsync(cancellationToken);
    }

}