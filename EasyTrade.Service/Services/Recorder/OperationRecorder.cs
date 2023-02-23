using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services.Recorder;

public class OperationRecorder : IDataRecorder<UpdateBalanceModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private IRepository<Balance, string> _balanceRepository;
    private ILocker _locker;
    public OperationRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository,
        IRepository<Balance, string> balanceRepository, ILocker locker)
    {
        _db = db;
        _locker = locker;
        _ccyRepository = ccyRepository;
        _balanceRepository = balanceRepository;
    }
    public void Record(UpdateBalanceModel data)
    {
        var ccy = _ccyRepository.Get(data.IsoCode);
        var operation = new Operation()
        {
            Currency = ccy,
            DateTime = data.DateTime,
            Amount = data.Amount
        };
        _locker.Lock(() =>
        {
            _db.AddOperation(operation);
            _db.SaveChanges();
        });
    }

}