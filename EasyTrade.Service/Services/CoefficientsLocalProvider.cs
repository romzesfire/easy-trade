using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;


public class CoefficientsLocalProvider : ICoefficientProvider
{
    public decimal GetCoefficient(TradeOperation operation) => 1.2m;
}