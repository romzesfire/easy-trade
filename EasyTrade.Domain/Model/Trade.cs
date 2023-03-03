
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTrade.Domain.Model;

public abstract class Trade //Datetime + timezone
{
    private decimal buyAmount;
    private decimal sellAmount;
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public decimal BuyAmount
    { 
        get => buyAmount; 
        set => buyAmount = decimal.Round(value, 9, MidpointRounding.ToEven); 
    }
    public decimal SellAmount 
    { 
        get => sellAmount; 
        set => sellAmount = decimal.Round(value, 9, MidpointRounding.ToEven); 
    }
}
public class CurrencyTrade : Trade 
{
    public TradeType TradeType { get; set; }
    [Column(TypeName = "char(3)")]
    public string BuyCurrencyIso { get; set; }
    [Column(TypeName = "char(3)")]
    public string SellCurrencyIso { get; set; }
    
    [ForeignKey("BuyCurrencyIso")]
    public Currency BuyCcy { get; set; }
    
    [ForeignKey("SellCurrencyIso")]
    public Currency SellCcy { get; set; }
}