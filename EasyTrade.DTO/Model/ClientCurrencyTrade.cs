namespace EasyTrade.DTO.Model;

public class ClientCurrencyTrade : CurrencyTrade
{
    public uint BrokerCurrencyTradeId { get; set; }
    public BrokerCurrencyTrade BrokerCurrencyTrade { get; set; }
    
    public ClientCurrencyTrade(Currency buyCcy, Currency sellCcy, 
        decimal buyAmount,  decimal sellAmount, BrokerCurrencyTrade brokerTrade)
    {
        BuyCcy = buyCcy;
        SellCcy = sellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
        BrokerCurrencyTrade = brokerTrade;
    }
}