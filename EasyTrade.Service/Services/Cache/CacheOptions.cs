namespace EasyTrade.Service.Services.Cache;

public class CacheOptions
{
    public TimeSpan ValidTime { get; }

    public CacheOptions(int minutes)
    {
        ValidTime = TimeSpan.FromMinutes(minutes);
    }

    public CacheOptions(TimeSpan valid)
    {
        ValidTime = valid;
    }

    public static CacheOptions GetDefault()
    {
        return new CacheOptions(5);
    }
}