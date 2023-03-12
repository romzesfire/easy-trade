using System.Collections;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Services;
using EasyTrade.Test.Model;
using Moq;
using NUnit.Framework;

namespace EasyTrade.Test;

[TestFixture]
public class BrokerCurrencyTradeCreatorTests
{
    public static IEnumerable SellCases
    {
        get
        {
            yield return new TestCaseData(new BrokerCurrencySellTradeCreatorModel(
                new Currency(1, "Dollar USA", "USD"), 
                new Currency(2, "Russian Rouble", "RUB"),
                0.8m, 1000m, 800m
                )
            );
            
            yield return new TestCaseData(new BrokerCurrencySellTradeCreatorModel(
                    new Currency(2, "Russian Rouble", "RUB"),
                    new Currency(1, "Dollar USA", "USD"),
                    65m, 100m, 6500m
                )
            );
        }
    }
    
    public static IEnumerable BuyCases
    {
        get
        {
            yield return new TestCaseData(new BrokerCurrencyBuyTradeCreatorModel(
                    new Currency(1, "Dollar USA", "USD"), 
                    new Currency(2, "Russian Rouble", "RUB"),
                    0.2m, 1000m, 5000m
                )
            );
            
            yield return new TestCaseData(new BrokerCurrencyBuyTradeCreatorModel(
                    new Currency(2, "Russian Rouble", "RUB"),
                    new Currency(1, "Dollar USA", "USD"),
                    75m, 15000m, 200m
                )
            );
        }
    }
    
    
    [Test]
    [TestCaseSource(nameof(SellCases))]
    public async Task SellTradeCreationTests(BrokerCurrencySellTradeCreatorModel creatorModel)
    {
        var brokerTradeCreator = new BrokerCurrencyTradeCreator(creatorModel.QuotesProvider.Object,
            creatorModel.DomainCalculatorProvider);
        var trade = await brokerTradeCreator.Create(creatorModel.TradeCreationModel, creatorModel.BuyCurrency, creatorModel.SellCurrency);
        Assert.That(trade.BuyAmount == creatorModel.BuyAmountResult, 
            $"Broker trade must contain {creatorModel.BuyAmountResult} buy amount, but it was {trade.BuyAmount}");
    }
    
    [Test]
    [TestCaseSource(nameof(BuyCases))]
    public async Task BuyTradeCreationTests(BrokerCurrencyBuyTradeCreatorModel creatorModel)
    {
        var brokerTradeCreator = new BrokerCurrencyTradeCreator(creatorModel.QuotesProvider.Object,
            creatorModel.DomainCalculatorProvider);
        var trade = await brokerTradeCreator.Create(creatorModel.TradeCreationModel, creatorModel.BuyCurrency, creatorModel.SellCurrency);
        Assert.That(trade.SellAmount == creatorModel.SellAmountResult, 
            $"Broker trade must contain {creatorModel.SellAmountResult} buy amount, but it was {trade.SellAmount}");
    }
}