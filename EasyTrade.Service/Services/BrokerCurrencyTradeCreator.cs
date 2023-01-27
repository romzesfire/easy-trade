using System.ComponentModel.DataAnnotations;
using EasyTrade.DAL.Model;

namespace EasyTrade.Service.Services;
public interface IBrokerCurrencyTradeCreator
{
    public BrokerCurrencyTrade Create(string buyCcy, string sellCcy,
        decimal? buyAmount = null, decimal? sellAmount = null);
}

public class BrokerCurrencyTradeCreator : IBrokerCurrencyTradeCreator
{
    private IQuotesProvider _quotesProvider;
    private ICurrenciesProvider _currenciesProvider;
    
    public BrokerCurrencyTradeCreator(IQuotesProvider quotesProvider, ICurrenciesProvider currenciesProvider)
    {
        _quotesProvider = quotesProvider;
        _currenciesProvider = currenciesProvider;
    }
    
    public BrokerCurrencyTrade Create(string buyCcy, string sellCcy,
        decimal? buyAmount = null, decimal? sellAmount = null)
    {
        if (buyAmount == null && sellAmount == null)
            throw new ValidationException("It is necessary to specify amount of buying or selling currency.");

        if (IsValidCurrencies(buyCcy, sellCcy))
        {
            var quote = _quotesProvider.Get(sellCcy, buyCcy);
            
            if (sellAmount != null)
                buyAmount = sellAmount * quote.Price;
            
            else
                sellAmount = buyAmount / quote.Price;

            return new BrokerCurrencyTrade(_currenciesProvider.GetCurrencies().First(c=>c.IsoCode == buyCcy),
                _currenciesProvider.GetCurrencies().First(c=>c.IsoCode == sellCcy), 
                buyAmount.Value, sellAmount.Value);
        }
        else
        {
            throw new ValidationException("Currencies are not valid.");
        }
    }

    private bool IsValidCurrencies(string buyCcy, string sellCcy)
    {
        var availableCurrencies = _currenciesProvider.GetCurrencies();
        
        return availableCurrencies.Any(c => c.IsoCode == buyCcy) 
               && availableCurrencies.Any(c => c.IsoCode == sellCcy);
    }
}