using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Services.Cache;

namespace EasyTrade.Service.Services.Recorder;

public class OperationRecorder : IOperationRecorder
{
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _ccyRepository;
    private readonly ILocker _locker;
    private readonly IBalanceCalculator? _balanceCalculator;
    
    public OperationRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository,
         ILocker locker, IDomainCalculationProvider calculationProvider)
    {
        _db = db;
        _balanceCalculator = calculationProvider.Get<IBalanceCalculator>();
        _locker = locker;
        _ccyRepository = ccyRepository;
    }
    public async Task Record(UpdateBalanceModel data)
    {
        var ccy = await _ccyRepository.Get(data.IsoCode);
        var operation = new Operation()
        {
            Currency = ccy,
            DateTime = data.DateTime,
            Amount = data.Amount
        };
        Record(new [] { operation });
        await _db.SaveChangesAsync();
    }

    public void Record(IEnumerable<Operation> operations)
    {
        var ccys = operations.Select(o => o.Currency).Distinct();
        foreach (var ccy in ccys)
        {
            var balance = _db.Balances.FirstOrDefault(b=>b.CurrencyIso == ccy.IsoCode);
            _locker.ConcurrentExecuteAsync(() => 
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