
using EasyTrade.DAL.Model;
namespace EasyTrade.Service.Model.ResponseModels;

public class BalanceResponse
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public CurrencyResponse Currency { get; set; }
    public decimal Amount { get; set; }

    public static explicit operator BalanceResponse(Balance balance)
    {
        return new BalanceResponse()
        {
            Id = balance.Id,
            Currency = (CurrencyResponse)balance.Currency,
            DateTime = balance.DateTime,
            Amount = balance.Amount
        };
    }
}