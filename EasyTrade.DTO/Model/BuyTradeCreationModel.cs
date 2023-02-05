using System.ComponentModel.DataAnnotations;
using EasyTrade.API.Validation;

namespace EasyTrade.API.Model;

public class BuyTradeCreationModel : TradeCreationModel
{
    [GreaterThanZero]
    public decimal BuyCount { get; set; }
}