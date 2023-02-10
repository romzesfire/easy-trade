using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;


public class CoefficientsDbProvider : ICoefficientProvider
{
    private EasyTradeDbContext _db;
    public CoefficientsDbProvider(EasyTradeDbContext db)
    {
        _db = db;
    }
    public decimal GetCoefficient(TradeOperation operation)
    {
        return _db.GetCoefficients().FirstOrDefault(c => c.Operation == operation).Coefficient;
    }
}