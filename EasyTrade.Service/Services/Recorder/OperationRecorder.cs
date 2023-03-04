using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services.Recorder;

public class OperationRecorder : IOperationRecorder
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private IRepository<Balance, string> _balanceRepository;
    private ILocker _locker;
    private IBalanceCalculator _balanceCalculator;
    public OperationRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository,
        IRepository<Balance, string> balanceRepository, ILocker locker, IDomainCalculationProvider calculationProvider)
    {
        _db = db;
        _balanceCalculator = calculationProvider.Get<IBalanceCalculator>();
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
        Record(new [] { operation });
    }

    public void Record(IEnumerable<Operation> operations)
    {
        var ccys = operations.Select(o => o.Currency).Distinct();
        foreach (var ccy in ccys)
        {
            var balance = _db.Balances.FirstOrDefault(b=>b.CurrencyIso == ccy.IsoCode);
            _locker.ConcurrentExecute(() => 
                    AddOneCcyOperations(operations.Where(o=>o.Currency.IsoCode == ccy.IsoCode), ccy, balance),
                    balance
                );
        }
    }
    
    private void AddOneCcyOperations(IEnumerable<Operation> operations, Currency ccy, Balance balance)
    {
        balance = _balanceCalculator.Calculate(balance, operations, ccy);
        _db.Operations.AddRange(operations);

        if (balance.Id == -1)
            _db.Balances.AddRange(balance);
    }
}