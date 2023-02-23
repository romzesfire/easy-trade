namespace EasyTrade.Service.Exceptions;

public class AccountCurrencyNotFoundException : Exception
{
    public AccountCurrencyNotFoundException(string iso) : base($"Account for currency {iso} is not found")
    {
        
    }
}