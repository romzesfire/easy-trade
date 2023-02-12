using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ICoefficientProvider
{
    public TradeCoefficient GetCoefficient(TradeOperation operation);
}
