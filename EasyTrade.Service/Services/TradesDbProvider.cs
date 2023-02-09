using EasyTrade.DAL.DatabaseContext;
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


    public IEnumerable<Trade> GetTrades(int limit, int offset)
    {
        return _db.GetTrades(limit, offset);
    }

    public Trade GetTrade(uint id)
    {
        return _db.GetTrade(id);
    }
}