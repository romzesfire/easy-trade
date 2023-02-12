using System.ComponentModel.DataAnnotations;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;


public class BrokerCurrencyTradeCreator : IBrokerCurrencyTradeCreator
{
    private IQuotesProvider _quotesProvider;
    private ICurrenciesProvider _currenciesProvider;
    
    public BrokerCurrencyTradeCreator(IQuotesProvider quotesProvider, ICurrenciesProvider currenciesProvider)
    {
        _quotesProvider = quotesProvider;
        _currenciesProvider = currenciesProvider;
    }
    
    public BrokerCurrencyTrade Create(TradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy)
    {
        var quote = _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);
        if (tradeModel is BuyTradeCreationModel buyTradeModel)
        {
            var sellAmount = buyTradeModel.BuyCount / quote.Price;
            return new BrokerCurrencyTrade(buyCcy, sellCcy, 
                buyTradeModel.BuyCount, sellAmount, buyTradeModel.DateTime, TradeType.Buy);
        }

        if (tradeModel is SellTradeCreationModel sellTradeModel)
        {
            var buyAmount = sellTradeModel.SellCount * quote.Price;
            return new BrokerCurrencyTrade(buyCcy, sellCcy,  
                buyAmount, sellTradeModel.SellCount, sellTradeModel.DateTime, TradeType.Sell);
        }
        throw new NotImplementedException();
    }
}