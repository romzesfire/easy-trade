namespace EasyTrade.DTO.Model;

public class BrokerCurrencyTrade : CurrencyTrade
{
    public BrokerCurrencyTrade(Currency buyCcy, Currency sellCcy, 
        decimal buyAmount, decimal sellAmount)
    {
        BuyCcy = buyCcy;
        SellCcy = sellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
    }


}