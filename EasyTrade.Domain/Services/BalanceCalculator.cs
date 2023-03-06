using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;

namespace EasyTrade.Domain.Services;

public class BalanceCalculator : IBalanceCalculator
{
    public BalanceCalculator()
    {
        
    }
    public Balance Calculate(Balance? balance, IEnumerable<Operation> operations, Currency ccy)
    {
        var sum = operations.Sum(o => o.Amount);
        if (balance == null)
        {
            balance = new Balance() { Id = -1, Amount = 0, Currency = ccy, CurrencyIso = ccy.IsoCode };
        }

        sum += balance.Amount;
        if (sum < 0)
        {
            throw new NotEnoughAssetsException(operations.First().Currency.IsoCode);
        }

        balance.Amount = sum;
        balance.Version = Guid.NewGuid();
        return balance;
    }
}