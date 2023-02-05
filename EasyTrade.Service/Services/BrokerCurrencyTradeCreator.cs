using System.ComponentModel.DataAnnotations;
using EasyTrade.API.Model;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;
public interface IBrokerCurrencyTradeCreator
{
    public BrokerCurrencyTrade Create(TradeCreationModel tradeModel);
}

public class BrokerCurrencyTradeCreator : IBrokerCurrencyTradeCreator
{
    private IQuotesProvider _quotesProvider;
    private ICurrenciesProvider _currenciesProvider;
    
    public BrokerCurrencyTradeCreator(IQuotesProvider quotesProvider, ICurrenciesProvider currenciesProvider,
        ITradeSaver tradeSaver)
    {
        _quotesProvider = quotesProvider;
        _currenciesProvider = currenciesProvider;
    }
    
    public BrokerCurrencyTrade Create(TradeCreationModel tradeModel)
    {
        var quote = _quotesProvider.Get(tradeModel.SellCurrency, tradeModel.BuyCurrency);
        if (tradeModel is BuyTradeCreationModel buyTradeModel)
        {
                        
            
            return new BrokerCurrencyTrade(
                _currenciesProvider.GetCurrencies().First(c=>c.IsoCode == buyTradeModel.BuyCurrency),
                _currenciesProvider.GetCurrencies().First(c=>c.IsoCode == buyTradeModel.SellCurrency), 
                buyTradeModel.BuyCount, buyTradeModel.SellCount, buyTradeModel.DateTime);
        }

        if (tradeModel is SellTradeCreationModel sellTradeModel)
        {
            
            
            return new BrokerCurrencyTrade(_currenciesProvider.GetCurrencies().First(c=>c.IsoCode == buyCcy),
                _currenciesProvider.GetCurrencies().First(c=>c.IsoCode == sellCcy), 
                buyAmount.Value, sellAmount.Value);
        }
        
        // if (buyAmount == null && sellAmount == null)
        //     throw new ValidationException("It is necessary to specify amount of buying or selling currency.");
        // if
        // if (IsValidCurrencies(buyCcy, sellCcy))
        // {
        //     var quote = _quotesProvider.Get(sellCcy, buyCcy);
        //     
        //     if (sellAmount != null)
        //         buyAmount = sellAmount * quote.Price;
        //     
        //     else
        //         sellAmount = buyAmount / quote.Price;
        //
        //     return new BrokerCurrencyTrade(_currenciesProvider.GetCurrencies().First(c=>c.IsoCode == buyCcy),
        //         _currenciesProvider.GetCurrencies().First(c=>c.IsoCode == sellCcy), 
        //         buyAmount.Value, sellAmount.Value);
        // }
        // else
        // {
        //     throw new ValidationException("Currencies are not valid.");
        // }
    }

    private bool IsValidCurrencies(string buyCcy, string sellCcy)
    {
        var availableCurrencies = _currenciesProvider.GetCurrencies();
        
        return availableCurrencies.Any(c => c.IsoCode == buyCcy) 
               && availableCurrencies.Any(c => c.IsoCode == sellCcy);
    }
}