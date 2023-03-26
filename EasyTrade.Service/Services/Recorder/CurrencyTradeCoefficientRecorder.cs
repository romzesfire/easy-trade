using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Services.Cache;

namespace EasyTrade.Service.Services.Recorder;

public class CurrencyTradeCoefficientRecorder : IDataRecorder<UpdateCurrencyTradeCoefficientModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private readonly ILocker _locker;
    private readonly IRepository<CurrencyTradeCoefficient, (string, string)> _coefficients;
    private readonly object _lockObject = new();
    private readonly ICacheRepository<CurrencyTradeCoefficient, (string?, string?)> _cache;
    public CurrencyTradeCoefficientRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository, 
        ILocker locker, IRepository<CurrencyTradeCoefficient, (string, string)> coefficients, 
        ICacheServiceFactory cacheServiceFactory)
    {
        _db = db;
        _ccyRepository = ccyRepository;
        _locker = locker;
        _coefficients = coefficients;
        _cache = cacheServiceFactory.GetCacheService<CurrencyTradeCoefficient, (string?, string?)>(CacheType.Lock);
    }

    public async Task Record(UpdateCurrencyTradeCoefficientModel data)
    {
        var coefficient = await _coefficients.Get((data.FirstIsoCode, data.SecondIsoCode));
        if (coefficient.FirstCcy != null && coefficient.SecondCcy != null)
        {
            await _locker.ConcurrentExecuteAsync(() =>
            {
                coefficient.Coefficient = data.Coefficient;
                _cache.AddOrUpdate((coefficient.FirstCcy.IsoCode, coefficient.SecondCcy.IsoCode), coefficient);
                _db.SaveChanges();
            }, _lockObject);
        }
        else
        {
            var firstCcy = await _ccyRepository.Get(data.FirstIsoCode);
            var secondCcy = await _ccyRepository.Get(data.SecondIsoCode);
            var c = new CurrencyTradeCoefficient()
            {
                FirstCcy = firstCcy,
                SecondCcy = secondCcy,
                Coefficient = data.Coefficient,
                DateTime = data.DateTime
            };
            await _locker.ConcurrentExecuteAsync(() =>
            {
                _db.Coefficients.Add(c);
                _cache.AddOrUpdate((coefficient.FirstCcy.IsoCode, coefficient.SecondCcy.IsoCode), coefficient);
                _db.SaveChanges();
            }, _lockObject);
        }
    }
}