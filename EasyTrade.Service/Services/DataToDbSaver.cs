using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class DataToDbSaver : IDataSaver
{
    private EasyTradeDbContext _db;
    public DataToDbSaver(EasyTradeDbContext db)
    {
        _db = db;
    }
    public void Save()
    {
        _db.SaveChanges();
    }

    public void SaveAsync()
    {
        _db.SaveChangesAsync();
    }
}