using EasyTrade.Domain.Model;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Model.ResponseModels;

public class BalanceResponse
{
    public int Id { get; set; }
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