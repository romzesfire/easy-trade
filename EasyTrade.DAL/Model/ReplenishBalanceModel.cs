using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Validation;

namespace EasyTrade.DAL.Model;

public class ReplenishBalanceModel
{
    [MaxLength(3)]
    [MinLength(3)]
    [Required]
    public string IsoCode { get; set; }
    
    [Required]
    [GreaterThanZero]
    public decimal Amount { get; set; }
}