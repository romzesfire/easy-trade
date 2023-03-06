using EasyTrade.Domain.Abstractions;

namespace EasyTrade.Domain.Services;

public class DomainCalculatorProvider : IDomainCalculationProvider
{
    private readonly IEnumerable<ICalculator> _calculators;
    public DomainCalculatorProvider()
    {
        _calculators = new List<ICalculator>
        {
            new BalanceCalculator(),
            new PriceMarkupCalculator(),
            new QuotesCalculator()
        };
    }
    public T? Get<T>()
    {
        return _calculators.OfType<T>().FirstOrDefault();
    } 
    
}