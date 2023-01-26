namespace EasyTrade.Service.Services;

public interface ICoefficientProvider
{
    public decimal GetCoefficient();
}

public class CoefficientProvider : ICoefficientProvider
{
    public decimal GetCoefficient() => 1.2m;
}