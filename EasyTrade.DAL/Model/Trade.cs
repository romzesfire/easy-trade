


namespace EasyTrade.DAL.Model;

public abstract class Trade //Datetime + timezone
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
}
public class CurrencyTrade : Trade 
{
    public TradeType TradeType { get; set; }
    public Currency BuyCcy { get; set; }
    public Currency SellCcy { get; set; }
}