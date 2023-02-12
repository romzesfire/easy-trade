using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class BalanceDbProvider : IBalanceProvider
{
    private EasyTradeDbContext _db;
    
    public BalanceDbProvider(EasyTradeDbContext db)
    {
        _db = db;
    }
    public Balance GetBalance(string currencyIsoCode)
    {
        return _db.GetBalance(currencyIsoCode);
    }

    public Balance GetBalance(uint id)
    {
        return _db.GetBalance(id);
    }

    public IEnumerable<Balance> GetBalances(int limit, int offset)
    {
        return _db.GetBalances(limit, offset);
    }
}