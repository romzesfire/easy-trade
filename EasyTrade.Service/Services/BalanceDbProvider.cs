using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class BalanceDbProvider : IBalanceProvider
{
    private IRepository<Balance, int> _balanceRepository;
    public BalanceDbProvider(IRepository<Balance, int> balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public BalanceResponse GetBalance(string currencyIsoCode)
    {
        var balanceAmount = _balanceRepository.GetAll()
            .Where(b => b.Currency.IsoCode == currencyIsoCode).Sum(b=>b.Amount);
        var balance = (BalanceResponse)_balanceRepository.GetAll().Last();
        balance.Amount = balanceAmount;
        return balance;
    }

    public BalanceResponse GetOperation(int id)
    {
        return (BalanceResponse)_balanceRepository.Get(id);
    }

    public IEnumerable<BalanceResponse> GetOperations(int limit, int offset)
    {
        return _balanceRepository.GetLimited(limit, offset).Select(b=>(BalanceResponse)b);
    }
}