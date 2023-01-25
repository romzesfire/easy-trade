namespace EasyTrade.DAL.Model;

public abstract class Trade
{
    public uint Id { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
}
public class CurrencyTrade : Trade
{
    public Currency BuyCurrency { get; set; }
    public Currency SellCurrency { get; set; }
}