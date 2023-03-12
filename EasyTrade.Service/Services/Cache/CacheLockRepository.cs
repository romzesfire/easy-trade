namespace EasyTrade.Service.Services.Cache;

public class CacheLockRepository<TEnt, TId> : ICacheRepository<TEnt, TId>
{
    private readonly Dictionary<TId, CacheEntityModel<TEnt>> _cache;
    public CacheLockRepository()
    {
        _cache = new Dictionary<TId, CacheEntityModel<TEnt>>();
    }
    public TEnt Get(TId id, Func<TId, TEnt> getter)
    {
        if (!_cache.ContainsKey(id))
        {
            var entity = getter.Invoke(id);
            lock (entity)
            {
                _cache.Add(id, new CacheEntityModel<TEnt>(entity));
            }
            
            return entity;
        }
        else
        {
            var entity = _cache[id];
            lock (entity)
            {
                if (!entity.IsValid())
                {
                    entity.SetEntity(getter.Invoke(id));
                    return _cache[id].GetEntity();
                }

                return entity.GetEntity();
            }

        }
    }

    public void Clear()
    {
        _cache.Clear();
    }
}