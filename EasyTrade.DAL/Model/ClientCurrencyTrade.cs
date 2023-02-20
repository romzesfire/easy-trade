
namespace EasyTrade.DAL.Model;


public class ClientCurrencyTrade : CurrencyTrade
{
    public uint BrokerCurrencyTradeId { get; set; }
    public BrokerCurrencyTrade BrokerCurrencyTrade { get; set; }

    public ClientCurrencyTrade()
    {
        
    }
    public ClientCurrencyTrade(BrokerCurrencyTrade brokerTrade, decimal buyAmount, decimal sellAmount)
    {
        TradeType = brokerTrade.TradeType;
        BuyCcy = brokerTrade.BuyCcy;
        SellCcy = brokerTrade.SellCcy;
        BuyAmount = buyAmount;
        SellAmount = sellAmount;
        BrokerCurrencyTrade = brokerTrade;
        DateTime = brokerTrade.DateTime;
    }
}