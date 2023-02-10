namespace EasyTrade.Service.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(string isoCode) : base($"Not enough {isoCode} for this operation")
    { }
}