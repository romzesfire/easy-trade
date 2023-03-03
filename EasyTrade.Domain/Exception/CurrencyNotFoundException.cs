namespace EasyTrade.Domain.Exception;

public class CurrencyNotFoundException : System.Exception
{
    public CurrencyNotFoundException(string isoCode) : base($"Currency {isoCode} is not found")
    { }
}