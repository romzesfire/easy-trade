using EasyTrade.API.Validation;

namespace EasyTrade.API.Model;

public class SellTradeCreationModel : TradeCreationModel
{
    [GreaterThanZero]
    public decimal SellCount { get; set; }
}