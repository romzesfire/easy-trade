using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class BalanceDbProvider : IBalanceProvider
{
    private IRepository<Balance, string> _balanceRepository;
    public BalanceDbProvider(IRepository<Balance, string> balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public BalanceResponse GetBalance(string currencyIsoCode)
    {
        var balance = _balanceRepository.Get(currencyIsoCode);
        var balanceResponse = (BalanceResponse)balance;
        return balanceResponse;
    }

    public BalanceResponse GetOperation(int id)
    {
        return (BalanceResponse)_balanceRepository.GetAll().First(b=>b.Id == id);
    }

    public IEnumerable<BalanceResponse> GetOperations(int limit, int offset)
    {
        return _balanceRepository.GetLimited(limit, offset).Select(b=>(BalanceResponse)b);
    }
}