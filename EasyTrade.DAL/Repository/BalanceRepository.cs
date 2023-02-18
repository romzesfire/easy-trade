using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.DAL.Repository;

public class BalanceRepository : IRepository<Balance, string>
{
    private EasyTradeDbContext _db;

    public BalanceRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Balance> GetAll()
    {
        return _db.Balances
            .Include(b => b.Currency).ToList();
    }

    public IEnumerable<Balance> GetLimited(int limit, int offset)
    {
        return _db.Balances.Skip(offset).Take(limit)
            .Include(b => b.Currency).ToList();
    }

    public Balance Get(string id)
    {
        var operations = _db.Balances.Include(b => b.Currency)
            .Where(b => b.Currency.IsoCode == id);
        var balanceAmount = operations.Sum(o => o.Amount);
        var lastDate = operations.Max(o => o.DateTime);
        var lastOperation = operations.First(b=>b.DateTime == lastDate);
        var balance = new Balance()
        {
            Amount = balanceAmount,
            Currency = lastOperation.Currency,
            DateTime = lastOperation.DateTime
        }; 
        return balance;
    }
}