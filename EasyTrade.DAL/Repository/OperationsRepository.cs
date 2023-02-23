using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.DAL.Repository;

public class OperationsRepository : IRepository<Operation, int>
{
    private EasyTradeDbContext _db;

    public OperationsRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Operation> GetAll()
    {
        return _db.Operations
            .Include(b => b.Currency).ToList();
    }

    public (IEnumerable<Operation>, int) GetLimited(int limit, int offset)
    {
        return (_db.Operations.OrderByDescending(o=>o.DateTime).Skip(offset).Take(limit)
            .Include(b => b.Currency).ToList(), _db.Operations.Count());
    }

    public Operation Get(int id)
    {
        var operation = _db.Operations.Include(b => b.Currency)
            .FirstOrDefault(b => b.Id == id);

        return operation;
    }
}