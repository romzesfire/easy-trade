using System.Collections.Concurrent;

namespace EasyTrade.Service.Services.Cache;

public class CacheConcurrentRepository<TEnt, TId> : ICacheRepository<TEnt, TId>
{
    private readonly ConcurrentDictionary<TId, CacheEntityModel<TEnt>> _cache;
    private Func<TId, TEnt> _getter;
    public CacheConcurrentRepository()
    {
        _cache = new ConcurrentDictionary<TId, CacheEntityModel<TEnt>>();
    }
    public TEnt Get(TId id, Func<TId, TEnt> getter)
    {
        _getter = getter;
        var entity = _cache.AddOrUpdate(id, Add, Update);
        if (entity.IsValid())
            return entity.GetEntity();
        return entity.GetEntity();
    }

    private CacheEntityModel<TEnt> Update(TId id, CacheEntityModel<TEnt> entity)
    {
        if (entity.IsValid())
            return entity;
        return new CacheEntityModel<TEnt>(_getter.Invoke(id));
    }

    private CacheEntityModel<TEnt> Add(TId id)
    {
        return new CacheEntityModel<TEnt>(_getter.Invoke(id));
    }

    public void Clear()
    {
        _cache.Clear();
    }
}