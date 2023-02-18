using System.Text.Json.Serialization;
using EasyTrade.DAL.Model;


namespace EasyTrade.Service.Model.ResponseModels;

public class TradeResponse
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public decimal BuyAmount { get; set; }
    public decimal SellAmount { get; set; }
}

public class CurrencyTradeResponse : TradeResponse
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TradeType TradeType { get; set; }
    public CurrencyResponse BuyCcy { get; set; }
    public CurrencyResponse SellCcy { get; set; }
    
    public static explicit operator CurrencyTradeResponse(CurrencyTrade trade)
    {
        return new CurrencyTradeResponse()
        {
            BuyAmount = trade.BuyAmount,
            SellAmount = trade.SellAmount,
            BuyCcy = (CurrencyResponse)trade.BuyCcy,
            SellCcy = (CurrencyResponse)trade.SellCcy,
            DateTime = trade.DateTime,
            Id = trade.Id,
            TradeType = (TradeType)((int)trade.TradeType)
        };
    }
}