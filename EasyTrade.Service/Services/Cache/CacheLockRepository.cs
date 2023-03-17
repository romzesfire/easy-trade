namespace EasyTrade.Service.Services.Cache;

public class CacheLockRepository<TEnt, TId> : ICacheRepository<TEnt, TId>
{
    private readonly Dictionary<TId, CacheEntityModel<TEnt>> _cache;
    private object _lockObject = new object();

    public CacheLockRepository()
    {
        _cache = new Dictionary<TId, CacheEntityModel<TEnt>>();
    }

    public TEnt Get(TId id, Func<TId, TEnt> getter)
    {
        if (!_cache.ContainsKey(id))
        {
            var value = getter.Invoke(id);
            lock (_lockObject)
            {
                if (!_cache.ContainsKey(id))
                {
                    _cache.Add(id, new CacheEntityModel<TEnt>(value));
                    return value;
                }
            }
        }

        var entity = _cache[id];
        if (!entity.IsValid())
        {
            lock (_lockObject)
            {
                if (!entity.IsValid())
                {
                    entity.SetEntity(getter.Invoke(id));
                    return _cache[id].GetEntity();
                }
            }
        }

        return entity.GetEntity();
    }

    public void Clear()
    {
        _cache.Clear();
    }
}