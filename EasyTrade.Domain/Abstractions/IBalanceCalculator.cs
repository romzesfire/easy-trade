using EasyTrade.Domain.Model;

namespace EasyTrade.Domain.Abstractions;

public interface IBalanceCalculator : ICalculator
{
    public Balance Calculate(Balance balance, IEnumerable<Operation> operations, Currency ccy);
}