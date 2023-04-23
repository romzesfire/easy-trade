using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.ResponseModels;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class BalanceDbProvider : IBalanceProvider
{
    private readonly IRepository<Balance, string> _balanceRepository;
    public BalanceDbProvider(IRepository<Balance, string> balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<BalanceResponse> GetBalance(string currencyIsoCode, Guid userId)
    {
        var balance = await _balanceRepository.Get(currencyIsoCode, userId);
        var balanceResponse = (BalanceResponse)balance;
        return balanceResponse;
    }

    public (IEnumerable<BalanceResponse>, int) GetBalances(int limit, int offset, Guid userId)
    {
        var (balances, count) = _balanceRepository.GetLimited(limit, offset, userId);
        return (balances.Select(b => (BalanceResponse)b), count);
    }

}