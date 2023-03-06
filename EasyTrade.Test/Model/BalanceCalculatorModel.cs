using EasyTrade.Domain.Model;

namespace EasyTrade.Test.Model;

public class BalanceCalculatorModel
{
    public Balance? BalanceInput { get; set; }
    public IEnumerable<Operation> Operations { get; set; }
    public Currency Currency { get; set; }
}
public class BalanceCalculatorModelValid : BalanceCalculatorModel
{
    public Balance BalanceResult { get; set; }
}