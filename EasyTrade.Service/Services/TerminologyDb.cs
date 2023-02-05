using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Abstractions;

namespace EasyTrade.Service.Services;

public class TerminologyDb : ITerminologyApi
{
    private EasyTradeDbContext _dbContext;
    
    public TerminologyDb(EasyTradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Currency> GetAvailableCurrencies()
    {
        return _dbContext.GetCurrencies().Select(c=>new Currency(c.Id, c.Name, c.IsoCode));
    }
}