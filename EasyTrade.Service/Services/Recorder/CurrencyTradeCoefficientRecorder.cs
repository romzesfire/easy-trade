using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;

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

    public void Record(UpdateCurrencyTradeCoefficientModel data)
    {
        var firstCcy = _ccyRepository.Get(data.FirstIsoCode);
        var secondCcy = _ccyRepository.Get(data.SecondIsoCode);

        var c = new CurrencyTradeCoefficient()
        {
            FirstCcy = firstCcy,
            SecondCcy = secondCcy,
            Coefficient = data.Coefficient,
            DateTime = data.DateTime
        };

        _db.Coefficients.Add(c);
        _db.SaveChanges();
    }

}