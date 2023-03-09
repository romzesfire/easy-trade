namespace EasyTrade.Service.Services.Cache;

public class CacheLockRepository<TEnt, TId> : ICacheRepository<TEnt, TId>
{
    private readonly Dictionary<TId, CacheEntityModel<TEnt>> _cache;
    private readonly Func<TId, TEnt> _getter;
    public CacheLockRepository(Func<TId, TEnt> getter)
    {
        _cache = new Dictionary<TId, CacheEntityModel<TEnt>>();
        _getter = getter;
    }
    public TEnt Get(TId id)
    {
        if (!_cache.ContainsKey(id))
        {
            var entity = _getter.Invoke(id);
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
                    entity.SetEntity(_getter.Invoke(id));
                    return _cache[id].GetEntity();
                }

                return entity.GetEntity();
            }

        }
    }
}