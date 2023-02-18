using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
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

    public IEnumerable<Currency> GetLimited(int limit, int offset)
    {
        return _db.Currencies.Skip(offset).Take(limit).ToList();
    }

    public Currency Get(string id)
    {
        return _db.Currencies.First(c=>c.IsoCode == id);
    }
}