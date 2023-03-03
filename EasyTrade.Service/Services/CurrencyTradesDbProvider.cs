using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class CurrencyTradesDbProvider : ICurrencyTradesProvider
{
    private readonly IRepository<ClientCurrencyTrade, int> _tradeProvider;
    public CurrencyTradesDbProvider(IRepository<ClientCurrencyTrade, int> tradeProvider)
    {
        _tradeProvider = tradeProvider;
    }


    public IEnumerable<CurrencyTradeResponse> GetTrades(int limit, int offset)
    {
        return _tradeProvider.GetLimited(limit, offset).Item1.Select(c=>(CurrencyTradeResponse)c);
    }

    public CurrencyTradeResponse GetTrade(int id)
    {
        return (CurrencyTradeResponse)_tradeProvider.Get(id);
    }
}