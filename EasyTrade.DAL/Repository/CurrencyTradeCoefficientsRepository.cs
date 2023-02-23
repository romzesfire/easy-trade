using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.DAL.Repository;

public class CurrencyTradeCoefficientRepository : IRepository<CurrencyTradeCoefficient, (string?, string?)>
{
    private EasyTradeDbContext _db;
    public CurrencyTradeCoefficientRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public IEnumerable<CurrencyTradeCoefficient> GetAll()
    {
        return _db.Coefficients
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToList();
    }

    public (IEnumerable<CurrencyTradeCoefficient>, int) GetLimited(int limit, int offset)
    {
        return (_db.Coefficients.OrderByDescending(t=>t.DateTime).Skip(offset).Take(limit)
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToList(), _db.Coefficients.Count());
    }

    public CurrencyTradeCoefficient Get((string?, string?) id)
    {
        var c = _db.Coefficients.Include(c => c.FirstCcy)
            .Include(c => c.SecondCcy)
            .Where(c 
                => (id.Item2 == null && id.Item1 == null && c.FirstCcy == null && c.SecondCcy == null) ||
                   (c.FirstCcy.IsoCode == id.Item1 && c.SecondCcy.IsoCode == id.Item2) ||
                   (c.FirstCcy.IsoCode == id.Item2 && c.SecondCcy.IsoCode == id.Item1))
            .OrderByDescending(c=>c.DateTime).FirstOrDefault();
        if (c == null)
            return _db.Coefficients.Include(c => c.FirstCcy)
                .Include(c => c.SecondCcy)
                .Where(c
                    => c.FirstCcy == null && c.SecondCcy == null)
                .OrderByDescending(c=>c.DateTime).First();
        return c;
    }
}