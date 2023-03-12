using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;


public class BrokerCurrencyTradeCreator : IBrokerCurrencyTradeCreator
{
    private readonly IQuotesProvider _quotesProvider;
    private readonly IQuotesCalculator? _quotesCalculator;
    public BrokerCurrencyTradeCreator(IQuotesProvider quotesProvider, IDomainCalculationProvider calculationProvider)
    {
        _quotesProvider = quotesProvider;
        _quotesCalculator = calculationProvider.Get<IQuotesCalculator>();
    }
    
    public async Task<BrokerCurrencyTrade> Create(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy)
    {
        var quote = await _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);
        var sellAmount = _quotesCalculator.CalculateSellAmount(tradeModel.BuyCount, quote.Price); 
        
        return new BrokerCurrencyTrade(buyCcy, sellCcy, 
                tradeModel.BuyCount, sellAmount, tradeModel.DateTime, TradeType.Buy);
    }

    public async Task<BrokerCurrencyTrade> Create(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy)
    {
        var quote = await _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);
        var buyAmount = _quotesCalculator.CalculateBuyAmount(tradeModel.SellCount, quote.Price);
        return new BrokerCurrencyTrade(buyCcy, sellCcy,  
                buyAmount, tradeModel.SellCount, tradeModel.DateTime, TradeType.Sell);
    }

}