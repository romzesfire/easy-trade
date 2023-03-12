using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services.Recorder;

public class CurrencyTradeCoefficientRecorder : IDataRecorder<UpdateCurrencyTradeCoefficientModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    public CurrencyTradeCoefficientRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository)
    {
        _db = db;
        _ccyRepository = ccyRepository;

    }

    public async Task Record(UpdateCurrencyTradeCoefficientModel data)
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

        _db.Coefficients.Add(c);
        await _db.SaveChangesAsync();
    }

}