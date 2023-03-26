using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;
using EasyTrade.Service.Services.Cache;

namespace EasyTrade.Service.Services;

public class CurrencyTradeCoefficientsProvider : ICurrencyTradeCoefficientsProvider
{
    private readonly IRepository<CurrencyTradeCoefficient, (string?, string?)> _repo;
    private readonly ICacheRepository<CurrencyTradeCoefficientResponse, (string?, string?)> _cache;
    public CurrencyTradeCoefficientsProvider(IRepository<CurrencyTradeCoefficient, (string?, string?)> repo, 
        ICacheServiceFactory cacheFactory)
    {
        _repo = repo;
        _cache = cacheFactory.GetCacheService<CurrencyTradeCoefficientResponse, (string?, string?)>(CacheType.Lock);
    }

    public async Task<CurrencyTradeCoefficientResponse> GetCoefficient(string? firstIso, string? secondIso)
    {
        var c = _cache.Get((firstIso, secondIso), (ccys) => GetCoefficientFromDb(ccys).Result);
        return c;
    }
    public async Task<CurrencyTradeCoefficientResponse> GetCoefficientFromDb((string?, string?) currencies)
    {
        var c = await _repo.Get((currencies.Item1, currencies.Item2));
        return (CurrencyTradeCoefficientResponse)c;
    }
    
    public (IEnumerable<CurrencyTradeCoefficientResponse>, int) GetCoefficientsLimit(int limit, int offset)
    {
        var result = _repo.GetLimited(limit, offset);
        return (result.Item1
            .Select(c=>(CurrencyTradeCoefficientResponse)c), result.Item2);
    }
}