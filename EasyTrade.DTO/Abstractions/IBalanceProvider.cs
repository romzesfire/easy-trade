using EasyTrade.DAL.Model;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface IBalanceProvider
{
    public BalanceResponse GetBalance(string currencyIsoCode);
    public BalanceResponse GetOperation(int id);
    public IEnumerable<BalanceResponse> GetOperations(int limit, int offset);
}