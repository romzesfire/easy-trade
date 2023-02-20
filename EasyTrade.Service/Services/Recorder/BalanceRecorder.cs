using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;

namespace EasyTrade.Service.Services.Recorder;

public class BalanceRecorder : IDataRecorder<UpdateBalanceModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    public BalanceRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository)
    {
        _db = db;
        _ccyRepository = ccyRepository;

    }
    public void Record(UpdateBalanceModel data)
    {
        var ccy = _ccyRepository.Get(data.IsoCode);
        var balance = new Balance()
        {
            Currency = ccy,
            DateTime = data.DateTime,
            Amount = data.Amount
        };
        _db.Balances.Add(balance);
        _db.SaveChanges();
    }
}