using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model.Repository;

namespace EasyTrade.DAL.Repository;

public class CurrencyRepository : IRepository<Currency, string>
{
    private EasyTradeDbContext _db;
    public CurrencyRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Currency> GetAll()
    {
        return _db.Currencies.ToList();
    }

    public (IEnumerable<Currency>, int) GetLimited(int limit, int offset)
    {
        return (_db.Currencies.OrderByDescending(o=>o.Id).Skip(offset).Take(limit).ToList(), _db.Currencies.Count());
    }

    public Currency Get(string id)
    {
        var ccy = _db.Currencies.FirstOrDefault(c=>c.IsoCode == id);
        if (ccy == null)
            throw new CurrencyNotFoundException(id);
        
        return ccy;
    }
}