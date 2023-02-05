using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class CurrenciesDbValidator : ICurrenciesValidator
{
    private EasyTradeDbContext _db;
    
    public CurrenciesDbValidator(EasyTradeDbContext db)
    {
        _db = db;
    }
    public bool IsValidCcy(string iso)
    {
        return _db.GetCurrencies().Any(c => c.IsoCode == iso);
    }
}