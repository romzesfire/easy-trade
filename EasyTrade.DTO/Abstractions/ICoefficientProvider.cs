using EasyTrade.Domain.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ICoefficientProvider
{
    public Task<TradeCoefficient> GetCoefficient(TradeOperation operation);
}
