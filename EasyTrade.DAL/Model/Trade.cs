using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTrade.DAL.Model;

public abstract class Trade
{
    public uint Id { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
}
public class CurrencyTrade : Trade
{
    public uint BuyCcyId { get; set; }
    public uint SellCcyId { get; set; }
    public Currency BuyCcy { get; set; }
    public Currency SellCcy { get; set; }
}