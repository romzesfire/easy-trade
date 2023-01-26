using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
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
        return _dbContext.GetCurrencies();
    }
}