using EasyTrade.DTO.Model;

namespace EasyTrade.DAL.Model;

public class BrokerCurrencyTrade : CurrencyTrade, ICloneable
{
    public BrokerCurrencyTrade(){}
    public BrokerCurrencyTrade(Currency buyCcy, Currency sellCcy, 
        decimal buyAmount, decimal sellAmount, DateTimeOffset dateTime)
    {
        BuyCcy = buyCcy;
        SellCcy = sellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
        BuyCcyId = buyCcy.Id;
        SellCcyId = sellCcy.Id;
        DateTime = dateTime;
    }


    public object Clone()
    {
        return new BrokerCurrencyTrade()
        {
            BuyCcy = BuyCcy,
            SellCcy = SellCcy,
            BuyAmount = BuyAmount,
            SellAmount = BuyAmount,
            BuyCcyId = BuyCcyId,
            SellCcyId = SellCcyId
        };
    }
}