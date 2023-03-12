using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EasyTrade.Repositories.Repository;

public class CurrencyRepository : IRepository<Currency, string>
{
    private EasyTradeDbContext _db;
    private ICacheRepository<Currency, string> _cache;
    private IMemoryCache _memoryCache;
    public CurrencyRepository(EasyTradeDbContext db, IMemoryCache memoryCache, ICacheServiceFactory cacheServiceFactory)
    {
        _db = db;
        _cache = cacheServiceFactory.GetCacheService<Currency, string>(CacheType.Lock);
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Currency>> GetAll()
    {
        return await _db.Currencies.ToListAsync();
    }

    public (IEnumerable<Currency>, int) GetLimited(int limit, int offset)
    {
        return (_db.Currencies.OrderByDescending(o=>o.Id).Skip(offset).Take(limit).ToList(), _db.Currencies.Count());
    }

    private async Task<Currency> GetFromDb(string id)
    {
        var ccy = _db.Currencies.FirstOrDefault(c=>c.IsoCode == id);
        if (ccy == null)
            throw new CurrencyNotFoundException(id);
        
        return ccy;
    }
    public async Task<Currency> Get(string id)
    {
        _memoryCache.TryGetValue(id, out Currency ccy);
        //var ccy = await _cache.Get(id, GetFromDb);
        if (ccy == null)
        {
            ccy = await GetFromDb(id);
            if (ccy != null)
            {
                _memoryCache.Set(id, ccy,
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));
            }
        }
        return ccy;
    }
}