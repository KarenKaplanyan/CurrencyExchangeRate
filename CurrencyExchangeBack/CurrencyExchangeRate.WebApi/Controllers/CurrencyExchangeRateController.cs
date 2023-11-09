using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyExchangeRateController
{
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;

    public CurrencyExchangeRateController(ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
    }
    
    [Authorize(Roles = "user,admin")]
    [HttpGet("currencies")]
    public async Task<IEnumerable<CurrencySuggestDto>> GetCurrenciesAsync(
        [FromQuery] int pageIndex,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateRepository.GetCurrenciesAsync(new PagingParameters
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        }, cancellationToken);
    }
    
    [Authorize(Roles = "user, admin")]
    [HttpGet("currency")]
    public async Task<ActionResult<CurrencyDto>> GetCurrencyById([FromQuery] string currencyId, CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateRepository.GetCurrencyAsync(currencyId, cancellationToken);
    }

    [Authorize(Roles = "user, admin")]
    [HttpGet("totalCount")]
    public async Task<int> GetCurrencyTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _currencyExchangeRateRepository.GetTotalCountAsync(cancellationToken);
    }

}