using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.Repositories.Repository;

public class OperationsRepository : IRepository<Operation, int>
{
    private EasyTradeDbContext _db;

    public OperationsRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Operation>> GetAll()
    {
        return await _db.Operations
            .Include(b => b.Currency).ToListAsync();
    }

    public (IEnumerable<Operation>, int) GetLimited(int limit, int offset, Guid userId)
    {
        return (_db.Operations.OrderByDescending(o=>o.DateTime).Skip(offset).Take(limit)
            .Where(b=>b.AccountId == userId).Include(b => b.Currency)
            .ToList(), _db.Operations.Count());
    }

    public Task<Operation> Get(int id, Guid userId)
    {
        var operation = _db.Operations.Include(b => b.Currency)
            .FirstOrDefaultAsync(b => b.Id == id && b.AccountId == userId);

        return operation;
    }
}