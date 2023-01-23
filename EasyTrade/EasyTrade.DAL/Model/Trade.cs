namespace EasyTrade.DAL.Model;

public abstract class Trade
{
    public uint Id { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
    public Ccy BuyCcy { get; set; }
    public Ccy SellCcy { get; set; }
}
