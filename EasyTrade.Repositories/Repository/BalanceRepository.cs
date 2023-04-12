using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.Repositories.Repository;

public class BalanceRepository : IRepository<Balance, string>
{
    private EasyTradeDbContext _db;

    public BalanceRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Balance>> GetAll()
    {
        return await _db.Balances
            .Include(b => b.Currency).ToListAsync();
    }

    public (IEnumerable<Balance>, int) GetLimited(int limit, int offset, Guid userId)
    {
        return (_db.Balances.OrderBy(o=>o.Id).Skip(offset).Take(limit)
            .Include(b => b.Currency).ToList(), _db.Operations.Count());
    }

    public async Task<Balance> Get(string id, Guid userId)
    {
        var balance = await _db.Balances.Include(b => b.Currency)
            .FirstOrDefaultAsync(b => b.Currency.IsoCode == id && b.AccountId == userId);

        return balance;
    }
}