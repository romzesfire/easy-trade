using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ICoefficientProvider
{
    public decimal GetCoefficient(TradeOperation operation);
}
