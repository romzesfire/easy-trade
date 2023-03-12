using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.Repositories.Repository;

public class CurrencyTradeRepository : IRepository<ClientCurrencyTrade, int>
{
    private EasyTradeDbContext _db;
    public CurrencyTradeRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ClientCurrencyTrade>> GetAll()
    {
        return await _db.ClientTrades.Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).ToListAsync();
    }

    public (IEnumerable<ClientCurrencyTrade>, int) GetLimited(int limit, int offset)
    {
        return (_db.ClientTrades.OrderByDescending(t=>t.DateTime)
            .Skip(offset).Take(limit).Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).ToList(), _db.ClientTrades.Count());
    }

    public async Task<ClientCurrencyTrade> Get(int id)
    {
        return await _db.ClientTrades.Where(t => t.Id == id)
            .Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).FirstAsync();
    }
}