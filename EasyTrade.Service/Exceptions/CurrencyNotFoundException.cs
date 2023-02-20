namespace EasyTrade.Service.Exceptions;

public class CurrencyNotFoundException : Exception
{
    public CurrencyNotFoundException(string isoCode) : base($"Currency {isoCode} is not found")
    { }
}