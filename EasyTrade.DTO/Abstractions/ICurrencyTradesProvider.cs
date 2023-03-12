using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface ICurrencyTradesProvider
{
    public (IEnumerable<CurrencyTradeResponse>, int) GetTrades(int limit, int offset);
    public Task<CurrencyTradeResponse> GetTrade(int id);
}