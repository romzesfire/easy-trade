using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services.Recorder;

public class BalanceRecorder : IDataRecorder<UpdateBalanceModel>
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private IRepository<Balance, string> _balanceRepository;
    public BalanceRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository,
        IRepository<Balance, string> balanceRepository)
    {
        _db = db;
        _ccyRepository = ccyRepository;
        _balanceRepository = balanceRepository;
    }
    public void Record(UpdateBalanceModel data)
    {
        var ccy = _ccyRepository.Get(data.IsoCode);
        ValidateBalance(ccy, data.Amount);
        var balance = new Balance()
        {
            Currency = ccy,
            DateTime = data.DateTime,
            Amount = data.Amount
        };
        
        _db.Balances.Add(balance);
        _db.SaveChanges();
    }
    private void ValidateBalance(Currency sellCcy, decimal amount)
    {
        var balance = _balanceRepository.Get(sellCcy.IsoCode);
        if (balance.Amount + amount <= 0)
            throw new NotEnoughAssetsException(sellCcy.IsoCode);
    }
}