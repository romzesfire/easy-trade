using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Services.Cache;
using System.Transactions;

namespace EasyTrade.Service.Services.Recorder;

public class CurrencyTradeCoefficientRecorder : IDataRecorder<UpdateCurrencyTradeCoefficientModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private readonly ILocker _locker;
    private readonly IRepository<CurrencyTradeCoefficient, (string, string)> _coefficients;
    private static object _lockObject = new();
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

    public async Task Record(UpdateCurrencyTradeCoefficientModel data, Guid username = default)
    {
        await using (var txn = await _db.Database.BeginTransactionAsync())
        {
            
            var coefficient = await _coefficients.Get((data.FirstIsoCode, data.SecondIsoCode));
            if (coefficient.FirstCcy != null && coefficient.SecondCcy != null)
            {
                await _locker.ConcurrentExecuteAsync(() =>
                {
                    coefficient.Coefficient = data.Coefficient;
                    _db.SaveChanges();
                    txn.Commit();
                    _cache.AddOrUpdate((coefficient.FirstCcy.IsoCode, coefficient.SecondCcy.IsoCode), coefficient);
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
                    _db.SaveChanges();
                    txn.Commit();
                    _cache.AddOrUpdate((coefficient.FirstCcy.IsoCode, coefficient.SecondCcy.IsoCode), coefficient);
                }, _lockObject);
            }
        }
    }


}