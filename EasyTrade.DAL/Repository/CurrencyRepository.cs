using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Exceptions;

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

    public IEnumerable<Currency> GetLimited(int limit, int offset)
    {
        return _db.Currencies.Skip(offset).Take(limit).ToList();
    }

    public Currency Get(string id)
    {
        var ccy = _db.Currencies.FirstOrDefault(c=>c.IsoCode == id);
        if (ccy == null)
            throw new CurrencyNotFoundException(id);
        
        return ccy;
    }
}