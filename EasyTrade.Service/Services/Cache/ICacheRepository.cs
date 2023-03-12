namespace EasyTrade.Service.Services.Cache;

public interface ICacheRepository
{
    public void Clear();
}

public interface ICacheRepository<TEnt, TId> : ICacheRepository
{
    public TEnt Get(TId id, Func<TId, TEnt> getter);
}