using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.DAL.Repository;

public class BalanceRepository : IRepository<Balance, int>
{
    private EasyTradeDbContext _db;

    public BalanceRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Balance> GetAll()
    {
        return _db.balances
            .Include(b => b.Currency).ToList();
    }

    public IEnumerable<Balance> GetLimited(int limit, int offset)
    {
        return _db.balances.Skip(offset).Take(limit)
            .Include(b => b.Currency).ToList();
    }

    public Balance Get(int id)
    {
        return _db.balances.Where(b => b.Id == id)
            .Include(b => b.Currency).First();
    }
}