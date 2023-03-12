using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Model.ResponseModels;
using EasyTrade.Service.Services;
using Moq;

namespace EasyTrade.Test.Model;

public class BrokerCurrencyTradeCreatorModel
{
    public Mock<IQuotesProvider> QuotesProvider { get;}
    public DomainCalculatorProvider DomainCalculatorProvider { get; }
    public Currency BuyCurrency { get; }
    public Currency SellCurrency { get; }
    
    public BrokerCurrencyTradeCreatorModel(Currency buyCurrency, Currency sellCurrency, 
        decimal price)
    {
        QuotesProvider = new Mock<IQuotesProvider>();
        QuotesProvider.Setup(q => q.Get(sellCurrency.IsoCode, buyCurrency.IsoCode))
            .ReturnsAsync(new Quote(
                new QuoteResponse()
                {
                    Query = new Query() { From = sellCurrency.IsoCode, To = buyCurrency.IsoCode },
                    Result = price
                }));
        BuyCurrency = buyCurrency;
        SellCurrency = sellCurrency;
        DomainCalculatorProvider = new DomainCalculatorProvider();
    }
}


public class BrokerCurrencyBuyTradeCreatorModel : BrokerCurrencyTradeCreatorModel
{
    public BuyTradeCreationModel TradeCreationModel { get; set; }
    public decimal SellAmountResult { get; set; }
    
    public BrokerCurrencyBuyTradeCreatorModel(Currency buyCurrency, Currency sellCurrency, 
        decimal price, decimal buyCount, decimal sellAmountResult) 
        : base(buyCurrency, sellCurrency, price)
    {
        SellAmountResult = sellAmountResult;
        TradeCreationModel = new BuyTradeCreationModel()
        {
            BuyCurrency = buyCurrency.IsoCode,
            SellCurrency = sellCurrency.IsoCode, 
            BuyCount = buyCount, 
            DateTime = DateTimeOffset.Now
        };
    }
}

public class BrokerCurrencySellTradeCreatorModel : BrokerCurrencyTradeCreatorModel
{
    public SellTradeCreationModel TradeCreationModel { get; }
    public decimal BuyAmountResult { get; }
    public BrokerCurrencySellTradeCreatorModel(Currency buyCurrency, Currency sellCurrency, 
        decimal price, decimal sellCount, decimal buyAmountResult) 
        : base(buyCurrency, sellCurrency, price)
    {
        BuyAmountResult = buyAmountResult;
        TradeCreationModel = new SellTradeCreationModel()
        {
            BuyCurrency = buyCurrency.IsoCode,
            SellCurrency = sellCurrency.IsoCode, 
            SellCount = sellCount, 
            DateTime = DateTimeOffset.Now
        };
    }

    
}