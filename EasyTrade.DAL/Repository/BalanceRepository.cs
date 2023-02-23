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

    public (IEnumerable<Balance>, int) GetLimited(int limit, int offset)
    {
        return (_db.Balances.OrderBy(o=>o.Id).Skip(offset).Take(limit)
            .Include(b => b.Currency).ToList(), _db.Operations.Count());
    }

    public Balance Get(string id)
    {
        var balance = _db.Balances.Include(b => b.Currency)
            .FirstOrDefault(b => b.Currency.IsoCode == id);

        return balance;
    }
}