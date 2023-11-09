using AutoMapper;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Infrastructure.Contexts;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Common;

public class CurrencyExchangeRateRepository: ICurrencyExchangeRateRepository
{
    private readonly CurrencyDbContext _currencyDbContext;
    private readonly IMapper _mapper;

    public CurrencyExchangeRateRepository(
        CurrencyDbContext currencyDbContext,
        IMapper mapper)
    {
        _currencyDbContext = currencyDbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CurrencySuggestDto>> GetCurrenciesAsync(PagingParameters parameters, CancellationToken cancellationToken )
    {
        return _mapper.Map<IEnumerable<CurrencySuggestDto>>(await _currencyDbContext.Currencies
            .ToListAsync(cancellationToken))
            .Skip(parameters.PageIndex * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    public async Task<CurrencyDto> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken)
    {
        return _mapper.Map<CurrencyDto>(await _currencyDbContext.Currencies
            .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken));
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _currencyDbContext.Currencies.CountAsync(cancellationToken);
    }
}