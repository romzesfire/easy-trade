namespace EasyTrade.Service.Services.Cache;


public enum CacheType
{
    Lock,
    Concurrent
}

public class CacheServiceFactory : ICacheServiceFactory
{
    private List<ICacheRepository> _cacheServices;

    public CacheServiceFactory()
    {
        _cacheServices = new List<ICacheRepository>();
    }

    public ICacheRepository<TEnt, TId> GetCacheService<TEnt, TId>(CacheType type)
    {
        var services = _cacheServices.OfType<ICacheRepository<TEnt, TId>>();
        if (services.Any())
        {
            return services.First();
        }

        ICacheRepository<TEnt, TId> service = type switch
        {
            CacheType.Concurrent => new CacheConcurrentRepository<TEnt, TId>(),
            CacheType.Lock => new CacheLockRepository<TEnt, TId>(),
            _ => throw new NotImplementedException()
        };
        _cacheServices.Add(service);

        return service;
    }
}