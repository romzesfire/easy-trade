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
    private IRepository<Balance, string> _balanceRepository;
    public OperationRecorder(EasyTradeDbContext db, IRepository<Currency, string> ccyRepository,
         ILocker locker, IDomainCalculationProvider calculationProvider, IRepository<Balance, string> balanceRepository)
    {
        _db = db;
        _balanceCalculator = calculationProvider.Get<IBalanceCalculator>();
        _locker = locker;
        _ccyRepository = ccyRepository;
        _balanceRepository = balanceRepository;
    }
    public async Task Record(UpdateBalanceModel data, Guid userId)
    {
        var ccy = await _ccyRepository.Get(data.IsoCode);
        var operation = new Operation()
        {
            Currency = ccy,
            DateTime = data.DateTime,
            Amount = data.Amount,
            AccountId = userId
        };
        Record(new [] { operation }, userId);
        await _db.SaveChangesAsync();
    }

    public async Task Record(IEnumerable<Operation> operations, Guid userId)
    {
        var ccys = operations.Select(o => o.Currency).Distinct();
        foreach (var ccy in ccys)
        {
            var balance =  _balanceRepository.Get(ccy.IsoCode, userId).Result;
            _locker.ConcurrentExecuteAsync(() => 
                    AddOneCcyOperations(operations.Where(o=>o.Currency.IsoCode == ccy.IsoCode), ccy, 
                        balance, userId), balance);
        }
    }
    
    private void AddOneCcyOperations(IEnumerable<Operation> operations, Currency ccy, Balance balance, Guid userId)
    {
        balance = _balanceCalculator.Calculate(balance, operations, ccy, userId);
        _db.Operations.AddRange(operations);

        if (balance.Id == -1)
            _db.Balances.AddRange(balance);
    }


}