using System.Collections.Concurrent;

namespace EasyTrade.Service.Services.Cache;

public class CacheEntityModel<TEnt>
{
    private TEnt _entity;
    private DateTime _updated;
    private CacheOptions _options;

    public CacheEntityModel(TEnt entity)
    {
        _entity = entity;
        _updated = DateTime.Now;
        _options = CacheOptions.GetDefault();
    }

    public CacheEntityModel(TEnt entity, CacheOptions options) : this(entity)
    {
        _options = options;
    }

    public TEnt GetEntity() => _entity;
    public void SetEntity(TEnt entity)
    {
        _entity = entity;
        _updated = DateTime.Now;
    }
    public bool IsValid()
    {
        var elapsedTime = DateTime.Now.Subtract(_updated);
        return elapsedTime < _options.ValidTime;
    }
}