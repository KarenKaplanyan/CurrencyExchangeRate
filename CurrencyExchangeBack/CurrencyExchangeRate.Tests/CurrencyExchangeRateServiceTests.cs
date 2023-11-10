using AutoMapper;
using CurrencyExchangeRate.Domain.Entities;
using CurrencyExchangeRate.Domain.Repositories;
using CurrencyExchangeRate.Infrastructure;
using CurrencyExchangeRate.WebApi.Dto;
using CurrencyExchangeRate.WebApi.Infrastructure.Dto;
using CurrencyExchangeRate.WebApi.Services;
using Moq;
using Xunit;


namespace CurrencyExchangeRate.Tests;

public class CurrencyExchangeRateServiceTests
{
    private readonly CurrencyExchangeRateService _currencyExchangeRateService;
    private readonly Mock<ICurrencyExchangeRateRepository> _currencyExchangeRateRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public CurrencyExchangeRateServiceTests()
    {
        _currencyExchangeRateRepositoryMock = new Mock<ICurrencyExchangeRateRepository>();
        _mapperMock = new Mock<IMapper>();
        _currencyExchangeRateService = new CurrencyExchangeRateService(
            _currencyExchangeRateRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task GetCurrenciesWithPagingAsync_ShouldReturnCurrencies()
    {
        // Arrange
        var paging = new PagingDto();
        var cancellationToken = new CancellationToken();
        var currencies = new List<Currency>();
        var expectedCurrencyDtos = new List<CurrencyDto>();

        _currencyExchangeRateRepositoryMock.Setup(repo => repo.GetCurrenciesWithPagingAsync(It.IsAny<PagingParameters>(), cancellationToken))
            .ReturnsAsync(currencies);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CurrencyDto>>(currencies))
            .Returns(expectedCurrencyDtos);

        // Act
        var result = await _currencyExchangeRateService.GetCurrenciesWithPagingAsync(paging, cancellationToken);

        // Assert
        Assert.Equal(expectedCurrencyDtos, result);
    }
    [Fact]
    public async Task GetCurrencyAsync_ShouldReturnCurrency()
    {
        // Arrange
        var currencyId = "R00123";
        var cancellationToken = new CancellationToken();
        var currency = new Currency();
        var expectedCurrencyDto = new CurrencyDto();

        _currencyExchangeRateRepositoryMock.Setup(repo => repo.GetCurrencyAsync(currencyId, cancellationToken))
            .ReturnsAsync(currency);
        _mapperMock.Setup(mapper => mapper.Map<CurrencyDto>(currency))
            .Returns(expectedCurrencyDto);

        // Act
        var result = await _currencyExchangeRateService.GetCurrencyAsync(currencyId, cancellationToken);

        // Assert
        Assert.Equal(expectedCurrencyDto, result);
    }
    [Fact]
    public async Task GetCurrencyTotalAsync_ShouldReturnTotalCurrency()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var totalCount = 10;
        var expectedCurrencyDto = new CurrencyDto();

        _currencyExchangeRateRepositoryMock.Setup(repo => repo.GetTotalCountAsync(cancellationToken))
            .ReturnsAsync(totalCount);

        // Act
        var result = await _currencyExchangeRateService.GetTotalCountAsync(cancellationToken);

        // Assert
        Assert.Equal(totalCount, result);
    }
    
}