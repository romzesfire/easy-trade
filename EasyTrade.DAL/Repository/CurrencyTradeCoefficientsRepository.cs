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
        return _db.coefficients
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToList();
    }

    public IEnumerable<CurrencyTradeCoefficient> GetLimited(int limit, int offset)
    {
        return _db.coefficients.Skip(offset).Take(limit)
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToList();
    }

    public CurrencyTradeCoefficient Get((string?, string?) id)
    {
        return _db.coefficients.Include(c => c.FirstCcy)
            .Include(c => c.SecondCcy)
            .First(c 
                => (id.Item2 == null && id.Item1 == null && c.FirstCcy == null && c.SecondCcy == null) ||
                   (c.FirstCcy.IsoCode == id.Item1 && c.SecondCcy.IsoCode == id.Item2) ||
                   (c.FirstCcy.IsoCode == id.Item2 && c.SecondCcy.IsoCode == id.Item1));
    }
}