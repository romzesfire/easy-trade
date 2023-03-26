using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services.Recorder;

public class CurrencyTradeCoefficientRecorder : IDataRecorder<UpdateCurrencyTradeCoefficientModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private readonly ILocker _locker;
    private readonly IRepository<CurrencyTradeCoefficient, (string, string)> _coefficients;
    private readonly object _lockObject = new();
    public CurrencyTradeCoefficientRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository, 
        ILocker locker, IRepository<CurrencyTradeCoefficient, (string, string)> coefficients)
    {
        _db = db;
        _ccyRepository = ccyRepository;
        _locker = locker;
        _coefficients = coefficients;
    }

    public async Task Record(UpdateCurrencyTradeCoefficientModel data)
    {
        var coefficient = await _coefficients.Get((data.FirstIsoCode, data.SecondIsoCode));
        if (coefficient.FirstCcy != null && coefficient.SecondCcy != null)
        {
            await _locker.ConcurrentExecuteAsync(() =>
            {
                coefficient.Coefficient = data.Coefficient;
                _db.SaveChangesAsync();
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
                _db.SaveChangesAsync();
            }, _lockObject);
        }
    }
}