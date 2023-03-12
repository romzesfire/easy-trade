using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.Repositories.Repository;

public class CurrencyTradeCoefficientRepository : IRepository<CurrencyTradeCoefficient, (string?, string?)>
{
    private EasyTradeDbContext _db;
    public CurrencyTradeCoefficientRepository(EasyTradeDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CurrencyTradeCoefficient>> GetAll()
    {
        return await _db.Coefficients
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToListAsync();
    }

    public (IEnumerable<CurrencyTradeCoefficient>, int) GetLimited(int limit, int offset)
    {
        return (_db.Coefficients.OrderByDescending(t=>t.DateTime).Skip(offset).Take(limit)
            .Include(c=>c.FirstCcy)
            .Include(c=>c.SecondCcy)
            .ToList(), _db.Coefficients.Count());
    }

    public async Task<CurrencyTradeCoefficient> Get((string?, string?) id)
    {
        var coefficient = await _db.Coefficients.Include(cf => cf.FirstCcy)
            .Include(cf => cf.SecondCcy)
            .Where(cf 
                => (id.Item2 == null && id.Item1 == null && cf.FirstCcy == null && cf.SecondCcy == null) ||
                   (cf.FirstCcy.IsoCode == id.Item1 && cf.SecondCcy.IsoCode == id.Item2) ||
                   (cf.FirstCcy.IsoCode == id.Item2 && cf.SecondCcy.IsoCode == id.Item1))
            .OrderByDescending(c=>c.DateTime).FirstOrDefaultAsync();
        if (coefficient == null)
            return await _db.Coefficients.Include(cf => cf.FirstCcy)
                .Include(cf => cf.SecondCcy)
                .Where(cf
                    => cf.FirstCcy == null && cf.SecondCcy == null)
                .OrderByDescending(cf=>cf.DateTime).FirstAsync();
        return coefficient;
    }
}