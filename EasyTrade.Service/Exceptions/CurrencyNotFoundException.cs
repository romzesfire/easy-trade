namespace EasyTrade.Service.Exceptions;

public class CurrencyNotFoundException : NotFoundException
{
    public CurrencyNotFoundException(string isoCode) : base($"Currency {isoCode} is not found")
    { }
}