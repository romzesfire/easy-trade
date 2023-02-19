using System.ComponentModel.DataAnnotations;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;


public class BrokerCurrencyTradeCreator : IBrokerCurrencyTradeCreator
{
    private IQuotesProvider _quotesProvider;
    private ICurrenciesProvider _currenciesProvider;
    
    public BrokerCurrencyTradeCreator(IQuotesProvider quotesProvider)
    {
        _quotesProvider = quotesProvider;
    }
    
    public BrokerCurrencyTrade Create(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy)
    {
        var quote = _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);
        var sellAmount = tradeModel.BuyCount / quote.Price;
        
        return new BrokerCurrencyTrade(buyCcy, sellCcy, 
                tradeModel.BuyCount, sellAmount, tradeModel.DateTime, TradeType.Buy);
    }

    public BrokerCurrencyTrade Create(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy)
    {
        var quote = _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);

        var buyAmount = tradeModel.SellCount * quote.Price;
        return new BrokerCurrencyTrade(buyCcy, sellCcy,  
                buyAmount, tradeModel.SellCount, tradeModel.DateTime, TradeType.Sell);
    }
}