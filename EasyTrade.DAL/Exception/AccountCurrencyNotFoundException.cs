namespace EasyTrade.Service.Exceptions;

public class AccountCurrencyNotFoundException : Exception
{
    // у exception - место в Service или отдельной сборке БЛ
    // судя по неймспейсу - он там и был когда-то :)
    public AccountCurrencyNotFoundException(string iso) : base($"Account for currency {iso} is not found")
    {
        
    }
}