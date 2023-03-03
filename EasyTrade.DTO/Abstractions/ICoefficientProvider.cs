using EasyTrade.Domain.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ICoefficientProvider
{
    public TradeCoefficient GetCoefficient(TradeOperation operation);
}
