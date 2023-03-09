namespace EasyTrade.Service.Services.Cache;

public interface ICacheRepository<out TEnt, in TId>
{
    public TEnt Get(TId id);
}