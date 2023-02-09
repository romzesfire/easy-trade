using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Validation;

namespace EasyTrade.DTO.Model;

public class BuyTradeCreationModel : TradeCreationModel
{
    [GreaterThanZero]
    public decimal BuyCount { get; set; }
}