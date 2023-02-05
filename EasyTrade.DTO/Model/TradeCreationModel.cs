using System.ComponentModel.DataAnnotations;

namespace EasyTrade.API.Model;

public class TradeCreationModel
{
    [MaxLength(3)]
    [Required(ErrorMessage = "Buy currency is required")]
    public string BuyCurrency { get; set; }
    
    [MaxLength(3)]
    [Required(ErrorMessage = "Sell currency is required")]
    public string SellCurrency { get; set; }
    
    [Required]
    public DateTimeOffset DateTime { get; set; }
}