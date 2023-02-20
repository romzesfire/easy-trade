using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class CurrencyTradesDbProvider : ICurrencyTradesProvider
{
    private readonly IRepository<CurrencyTrade, int> _tradeProvider;
    public CurrencyTradesDbProvider(IRepository<CurrencyTrade, int> tradeProvider)
    {
        _tradeProvider = tradeProvider;
    }


    public IEnumerable<CurrencyTradeResponse> GetTrades(int limit, int offset)
    {
        return _tradeProvider.GetLimited(limit, offset).Select(c=>(CurrencyTradeResponse)c);
    }

    public CurrencyTradeResponse GetTrade(int id)
    {
        return (CurrencyTradeResponse)_tradeProvider.Get(id);
    }
}