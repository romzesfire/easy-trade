using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;

public class TradesDbProvider : ITradesProvider
{
    private EasyTradeDbContext _db;
    public TradesDbProvider(EasyTradeDbContext db)
    {
        _db = db;
    }


    public IEnumerable<CurrencyTrade> GetTrades(int limit, int offset)
    {
        return _db.GetTrades(limit, offset).Select(c=>(CurrencyTrade)c);
    }

    public CurrencyTrade GetTrade(uint id)
    {
        return (CurrencyTrade)_db.GetTrade(id);
    }
}