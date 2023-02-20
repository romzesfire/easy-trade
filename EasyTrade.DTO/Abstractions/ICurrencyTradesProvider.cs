using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface ICurrencyTradesProvider
{
    public IEnumerable<CurrencyTradeResponse> GetTrades(int limit, int offset);
    public CurrencyTradeResponse GetTrade(int id);
}