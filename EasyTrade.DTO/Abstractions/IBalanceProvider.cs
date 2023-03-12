using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface IBalanceProvider
{
    public Task<BalanceResponse> GetBalance(string iso);
    public (IEnumerable<BalanceResponse>, int) GetBalances(int limit, int offset);
}