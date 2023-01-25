namespace EasyTrade.DAL.Model;

public class ClientCurrencyTrade : CurrencyTrade
{
    public BrokerCurrencyTrade BrokerCurrencyTrade { get; set; }
    public decimal Profit { get; set; }
}