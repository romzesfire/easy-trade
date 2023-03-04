namespace EasyTrade.Domain.Abstractions;

public interface IDomainCalculationProvider
{
    public T Get<T>();
}