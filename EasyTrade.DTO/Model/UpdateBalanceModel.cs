using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Validation;

namespace EasyTrade.DTO.Model;

public class UpdateBalanceModel
{
    [MaxLength(3)]
    [MinLength(3)]
    [Required]
    public string IsoCode { get; set; }
    
    [Required]
    [GreaterThanZero]
    public decimal Amount { get; set; }
    [Required]
    public DateTimeOffset DateTime { get; set; }
}