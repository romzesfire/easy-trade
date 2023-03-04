using EasyTrade.Domain.Abstractions;

namespace EasyTrade.Domain.Services;

public class PriceMarkupCalculator : IPriceMarkupCalculator
{
    public decimal CalculateSellAmount(decimal sellAmount, decimal coefficient) => sellAmount * coefficient;

    public decimal CalculateBuyAmount(decimal buyAmount, decimal coefficient) => buyAmount / coefficient;
}