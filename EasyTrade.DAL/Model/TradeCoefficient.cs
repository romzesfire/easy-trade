namespace EasyTrade.DAL.Model;

public enum TradeOperation
{
    CurrencyTrade
}

public class TradeCoefficient
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public decimal Coefficient { get; set; }
}

public class CurrencyTradeCoefficient : TradeCoefficient
{
    public Currency? FirstCcy { get; set; }
    public Currency? SecondCcy { get; set; }
}
