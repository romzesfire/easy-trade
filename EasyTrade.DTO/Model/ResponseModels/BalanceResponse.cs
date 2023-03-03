using EasyTrade.Domain.Model;

namespace EasyTrade.Service.Model.ResponseModels;

public class BalanceResponse
{
    public uint Id { get; set; }
    public CurrencyResponse Currency { get; set; }
    public decimal Amount { get; set; }

    public static explicit operator BalanceResponse(Balance operation)
    {
        return new BalanceResponse()
        {
            Id = operation.Id,
            Currency = (CurrencyResponse)operation.Currency,
            Amount = operation.Amount
        };
    }
}