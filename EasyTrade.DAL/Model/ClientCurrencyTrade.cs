namespace EasyTrade.DAL.Model;

public class ClientCurrencyTrade : CurrencyTrade
{
    public uint BrokerCurrencyTradeId { get; set; }
    public BrokerCurrencyTrade BrokerCurrencyTrade { get; set; }
    public ClientCurrencyTrade(){ }
    public ClientCurrencyTrade(Currency buyCcy, Currency sellCcy, 
        decimal buyAmount,  decimal sellAmount, BrokerCurrencyTrade brokerTrade)
    {
        BuyCcy = buyCcy;
        SellCcy = sellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
        BrokerCurrencyTrade = brokerTrade;
        BuyCcyId = buyCcy.Id;
        SellCcyId = sellCcy.Id;
        BrokerCurrencyTradeId = brokerTrade.Id;
    }
    
    public object Clone()
    {
        return new ClientCurrencyTrade()
        {
            BuyCcy = BuyCcy,
            SellCcy = SellCcy,
            BuyAmount = BuyAmount,
            SellAmount = BuyAmount,
            BuyCcyId = BuyCcyId,
            SellCcyId = SellCcyId,
            BrokerCurrencyTrade = BrokerCurrencyTrade,
        };
    }
}