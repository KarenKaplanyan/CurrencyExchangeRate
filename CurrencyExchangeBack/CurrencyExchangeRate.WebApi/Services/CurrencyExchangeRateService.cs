using AutoMapper;
using CurrencyExchangeRate.Domain.Repositories;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;

namespace CurrencyExchangeRate.WebApi.Services;

public class CurrencyExchangeRateService: ICurrencyExchangeRateService
{
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;
    private readonly IMapper _mapper;

    public CurrencyExchangeRateService(
        ICurrencyExchangeRateRepository currencyExchangeRateRepository,
        IMapper mapper)
    {
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CurrencyDto>> GetCurrenciesWithPagingAsync(PagingDto paging, CancellationToken cancellationToken)
    {
        var currencies = await _currencyExchangeRateRepository.GetCurrenciesWithPagingAsync(_mapper.Map<PagingParameters>(paging),
            cancellationToken);
        return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
    }

    public async Task<CurrencyDto> GetCurrencyAsync(string currencyId, CancellationToken cancellationToken)
    {
        var currency = _mapper.Map<CurrencyDto>(await _currencyExchangeRateRepository.GetCurrencyAsync(currencyId, cancellationToken));
        return currency;
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return _currencyExchangeRateRepository.GetTotalCountAsync(cancellationToken);
    }
}