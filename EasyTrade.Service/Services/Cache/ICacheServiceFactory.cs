namespace EasyTrade.Service.Services.Cache;

public interface ICacheServiceFactory
{
    ICacheRepository<TEnt, TId> GetCacheService<TEnt, TId>(CacheType type);
}