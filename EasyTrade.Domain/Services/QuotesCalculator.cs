using EasyTrade.Domain.Abstractions;

namespace EasyTrade.Domain.Services;

public class QuotesCalculator : IQuotesCalculator
{
    public decimal CalculateSellAmount(decimal buyAmount, decimal price) => buyAmount / price;
    public decimal CalculateBuyAmount(decimal sellAmount, decimal price) => sellAmount * price;
}