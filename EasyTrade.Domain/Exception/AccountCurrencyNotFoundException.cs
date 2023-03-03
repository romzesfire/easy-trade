namespace EasyTrade.Domain.Exception;

public class AccountCurrencyNotFoundException : System.Exception
{
    public AccountCurrencyNotFoundException(string iso) : base($"Account for currency {iso} is not found")
    {
        
    }
}