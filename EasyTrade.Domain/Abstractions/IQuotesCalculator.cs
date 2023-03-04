namespace EasyTrade.Domain.Abstractions;

public interface IQuotesCalculator : ICalculator
{
    public decimal CalculateSellAmount(decimal buyAmount, decimal price);
    public decimal CalculateBuyAmount(decimal sellAmount, decimal price);
}