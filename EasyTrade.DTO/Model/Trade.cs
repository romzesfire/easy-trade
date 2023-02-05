
namespace EasyTrade.DTO.Model;

public abstract class Trade //Datetime + timezone
{
    public DateTimeOffset DateTime { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
}
public class CurrencyTrade : Trade 
{
    public Currency BuyCcy { get; set; }
    public Currency SellCcy { get; set; }
}