
namespace EasyTrade.DAL.Model;

public class BrokerCurrencyTrade : CurrencyTrade
{
    public BrokerCurrencyTrade()
    {
        
    }
    public BrokerCurrencyTrade(Currency buyCcy, Currency sellCcy, 
        decimal buyAmount, decimal sellAmount, DateTimeOffset dateTimeOffset)
    {
        BuyCcy = buyCcy;
        SellCcy = sellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
        DateTime = dateTimeOffset;
    }


}