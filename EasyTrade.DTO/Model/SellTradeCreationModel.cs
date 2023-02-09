using EasyTrade.DTO.Validation;

namespace EasyTrade.DTO.Model;

public class SellTradeCreationModel : TradeCreationModel
{
    [GreaterThanZero]
    public decimal SellCount { get; set; }
}