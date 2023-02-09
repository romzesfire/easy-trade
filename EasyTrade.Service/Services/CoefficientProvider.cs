using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;


public class CoefficientProvider : ICoefficientProvider
{
    public decimal GetCoefficient() => 1.2m;
}