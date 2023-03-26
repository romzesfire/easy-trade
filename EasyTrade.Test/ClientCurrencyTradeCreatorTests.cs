using System.Collections;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Repositories.Repository;
using EasyTrade.Service.Services;
using EasyTrade.Test.Extension;
using EasyTrade.Test.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EasyTrade.Test;

[TestFixture]
public class ClientCurrencyTradeCreatorTests
{
    private static EasyTradeDbContext _dbContext = CreateDbContext();
    private static IRepository<Currency, string> _currencyRepository = new CurrencyRepository(_dbContext);

    private static EasyTradeDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<EasyTradeDbContext>()
            .UseInMemoryDatabase("EasyTradeDb").Options;
        var dbContext = new EasyTradeDbContext(options);
        dbContext.GenerateInMemoryData();
        return dbContext;
    }
    private static IEnumerable BuyCases
    {
        get
        {
            yield return new ClientCurrencyBuyTradeCreatorModel(
                _dbContext,
                new BuyTradeCreationModel()
                {
                    BuyCount = 100,
                    BuyCurrency = "RUB",
                    SellCurrency = "USD",
                    DateTime = DateTimeOffset.Now
                },
                100, 
                2M);
            
            yield return new ClientCurrencyBuyTradeCreatorModel(
                _dbContext,
                new BuyTradeCreationModel()
                {
                    BuyCount = 100,
                    BuyCurrency = "RUB",
                    SellCurrency = "EUR",
                    DateTime = DateTimeOffset.Now
                },
                100, 
                1.25M);
        }
        
    }
    
    private static IEnumerable SellCases
    {
        get
        {
            yield return new ClientCurrencySellTradeCreatorModel(
                _dbContext,
                new SellTradeCreationModel()
                {
                    SellCount = 100,
                    BuyCurrency = "RUB",
                    SellCurrency = "USD",
                    DateTime = DateTimeOffset.Now
                },
                100, 
                5000M);
            
            yield return new ClientCurrencySellTradeCreatorModel(
                _dbContext,
                new SellTradeCreationModel()
                {
                    SellCount = 100,
                    BuyCurrency = "RUB",
                    SellCurrency = "EUR",
                    DateTime = DateTimeOffset.Now
                },
                100, 
                8000M);
        }
        
    }
    

    [Test]
    [TestCaseSource(nameof(BuyCases))]
    public async Task TestCountOperationsAddingInClientCurrencyTradeCreator(ClientCurrencyBuyTradeCreatorModel model)
    {
        var countBefore = _dbContext.Operations.Count();
        var creator = new ClientCurrencyTradeCreator(
            model.BrokerTradeCreator, 
            model.Locker.Object,
            _dbContext,
            _currencyRepository,
            model.CoefficientRepository,
            model.OperationRecorder,
            new DomainCalculatorProvider()
        );
        await creator.Create(model.BuyTradeCreationModel);
        var countAfter = _dbContext.Operations.Count();
        Assert.That(countAfter - 2 == countBefore, "Invalid operations count added by creating trade");
    }
    
    [Test]
    [TestCaseSource(nameof(BuyCases))]
    public async Task TestOperationsAddingWithValidAmountInBuyOperation(ClientCurrencyBuyTradeCreatorModel model)
    {
        var creator = new ClientCurrencyTradeCreator(
            model.BrokerTradeCreator, 
            model.Locker.Object,
            _dbContext,
            _currencyRepository,
            model.CoefficientRepository,
            model.OperationRecorder,
            new DomainCalculatorProvider()
        );
        await creator.Create(model.BuyTradeCreationModel);

        var lastOperatons = _dbContext.Operations.OrderByDescending(o => o.Id).Take(2).ToList();
        var buyOperation = lastOperatons.FirstOrDefault(o=>o.Currency.IsoCode == model.BuyTradeCreationModel
            .BuyCurrency);
        var sellOperation = lastOperatons.FirstOrDefault(o=>o.Currency.IsoCode == model.BuyTradeCreationModel
            .SellCurrency);
        
        Assert.That(sellOperation.Amount == model.ExpectedSellAmount*(-1), 
            $"Added sell operation contains invalid amount. Expected {model.ExpectedSellAmount*(-1)}, but was {sellOperation.Amount}");
        Assert.That(buyOperation.Amount == model.BuyTradeCreationModel.BuyCount,
            $"Added buy operation contains invalid amount. Expected {model.BuyTradeCreationModel.BuyCount}, but was {buyOperation.Amount}");
        
    }
    
    [Test]
    [TestCaseSource(nameof(SellCases))]
    public async Task TestOperationsAddingWithValidAmountInSellOperation(ClientCurrencySellTradeCreatorModel model)
    {
        var creator = new ClientCurrencyTradeCreator(
            model.BrokerTradeCreator, 
            model.Locker.Object,
            _dbContext,
            _currencyRepository,
            model.CoefficientRepository,
            model.OperationRecorder,
            new DomainCalculatorProvider()
        );
        await creator.Create(model.SellTradeCreationModel);

        var lastOperatons = _dbContext.Operations.OrderByDescending(o => o.Id).Take(2).ToList();
        var buyOperation = lastOperatons.FirstOrDefault(o=>o.Currency.IsoCode == model.SellTradeCreationModel
            .BuyCurrency);
        var sellOperation = lastOperatons.FirstOrDefault(o=>o.Currency.IsoCode == model.SellTradeCreationModel
            .SellCurrency);
        Assert.That(sellOperation.Amount == model.SellTradeCreationModel.SellCount*(-1), 
            $"Added sell operation contains invalid amount. Expected {model.SellTradeCreationModel.SellCount*(-1)}, " +
            $"but was {sellOperation.Amount}");
        
        Assert.That(buyOperation.Amount == model.ExpectedBuyAmount,
            $"Added buy operation contains invalid amount. Expected {model.ExpectedBuyAmount}, but was {buyOperation.Amount}");

    }
    
}