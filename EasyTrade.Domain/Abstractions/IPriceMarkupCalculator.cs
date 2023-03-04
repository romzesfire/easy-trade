namespace EasyTrade.Domain.Abstractions;

public interface IPriceMarkupCalculator : ICalculator
{
    public decimal CalculateSellAmount(decimal sellAmount, decimal coefficient);
    public decimal CalculateBuyAmount(decimal buyAmount, decimal coefficient);
}