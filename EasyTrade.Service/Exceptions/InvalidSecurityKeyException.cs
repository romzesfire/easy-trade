namespace EasyTrade.Service.Exceptions;

public class InvalidSecurityKeyException : Exception
{
    public InvalidSecurityKeyException() : base("Invalid security key.") 
    {
        
    }
}