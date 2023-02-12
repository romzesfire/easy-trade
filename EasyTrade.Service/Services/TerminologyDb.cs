using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

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
        return _dbContext.GetCurrencies().ToList();
    }
}