using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IBalanceProvider
{
    public Balance GetBalance(string currencyIsoCode);
    public Balance GetBalance(uint id);
    public IEnumerable<Balance> GetBalances(int limit, int offset);
}