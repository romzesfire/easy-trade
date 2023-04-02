using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class CurrencyTradesDbProvider : ICurrencyTradesProvider
{
    private readonly IRepository<ClientCurrencyTrade, int> _tradeProvider;
    public CurrencyTradesDbProvider(IRepository<ClientCurrencyTrade, int> tradeProvider)
    {
        _tradeProvider = tradeProvider;
    }


    public (IEnumerable<CurrencyTradeResponse>, int) GetTrades(int limit, int offset, Guid userId)
    {
        var result = _tradeProvider.GetLimited(limit, offset, userId);
        return (result.Item1.Select(c=>(CurrencyTradeResponse)c), result.Item2);
    }

    public async Task<CurrencyTradeResponse> GetTrade(int id, Guid userId)
    {
        return (CurrencyTradeResponse) await _tradeProvider.Get(id, userId);
    }
}