using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EasyTrade.Repositories.Repository;

public class CurrencyRepository : IRepository<Currency, string>
{
    private EasyTradeDbContext _db;
    public CurrencyRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Currency>> GetAll()
    {
        return await _db.Currencies.ToListAsync();
    }

    public (IEnumerable<Currency>, int) GetLimited(int limit, int offset, Guid userId = default)
    {
        return (_db.Currencies.OrderByDescending(o=>o.Id).Skip(offset).Take(limit).ToList(), _db.Currencies.Count());
    }

    public async Task<Currency> Get(string id, Guid userId = default)
    {
        var ccy = await _db.Currencies.FirstOrDefaultAsync(c=>c.IsoCode == id);
        if (ccy == null)
            throw new CurrencyNotFoundException(id);
        
        return ccy;
    }
}