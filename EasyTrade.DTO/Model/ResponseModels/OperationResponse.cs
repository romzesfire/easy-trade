using EasyTrade.Domain.Model;

namespace EasyTrade.Service.Model.ResponseModels;

public class OperationResponse
{
    public uint Id { get; set; }
    public CurrencyResponse Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public static explicit operator OperationResponse(Operation operation)
    {
        return new OperationResponse()
        {
            Id = operation.Id,
            Currency = (CurrencyResponse)operation.Currency,
            Amount = operation.Amount,
            DateTime = operation.DateTime
        };
    }
}