using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTrade.Domain.Model;


public class Balance
{
    private decimal amount;
    public uint Id { get; set; }
    [ConcurrencyCheck]
    public Guid Version { get; set; }
    
    [Column(TypeName = "char(3)")]
    public string CurrencyIso { get; set; }
    
    [ForeignKey("CurrencyIso")]
    public Currency Currency { get; set; }
    public decimal Amount 
    { 
        get => amount; 
        set => amount = decimal.Round(value, 9, MidpointRounding.ToEven); 
    }
}