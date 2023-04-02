using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface ICurrencyTradesProvider
{
    public (IEnumerable<CurrencyTradeResponse>, int) GetTrades(int limit, int offset, Guid userId);
    public Task<CurrencyTradeResponse> GetTrade(int id, Guid userId);
}