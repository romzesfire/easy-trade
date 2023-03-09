using System.Collections.Concurrent;

namespace EasyTrade.Service.Services.Cache;

public class CacheConcurrentRepository<TEnt, TId> : ICacheRepository<TEnt, TId>
{
    private readonly ConcurrentDictionary<TId, CacheEntityModel<TEnt>> _cache;
    private readonly Func<TId, TEnt> _getter;
    public CacheConcurrentRepository(Func<TId, TEnt> getter)
    {
        _cache = new ConcurrentDictionary<TId, CacheEntityModel<TEnt>>();
        _getter = getter;
    }
    public TEnt Get(TId id)
    {
        var entity = _cache.GetOrAdd(id, Add);
        if(!entity.IsValid())
            _cache.AddOrUpdate(id, Add, Update);
        
        return _cache.GetOrAdd(id, Add).GetEntity();
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
}