namespace EasyTrade.DAL.Model;

public enum TradeOperation
{
    CurrencyTrade
}

public class TradeCoefficient
{
    public uint Id { get; set; }
    public TradeOperation Operation { get; set; }
    public decimal Coefficient { get; set; }
}