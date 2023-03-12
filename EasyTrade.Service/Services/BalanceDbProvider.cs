using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class BalanceDbProvider : IBalanceProvider
{
    private readonly IRepository<Balance, string> _balanceRepository;
    public BalanceDbProvider(IRepository<Balance, string> balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<BalanceResponse> GetBalance(string currencyIsoCode)
    {
        var balance = await _balanceRepository.Get(currencyIsoCode);
        var balanceResponse = (BalanceResponse)balance;
        return balanceResponse;
    }

    public (IEnumerable<BalanceResponse>, int) GetBalances(int limit, int offset)
    {
        var (balances, count) = _balanceRepository.GetLimited(limit, offset);
        return (balances.Select(b => (BalanceResponse)b), count);
    }

}