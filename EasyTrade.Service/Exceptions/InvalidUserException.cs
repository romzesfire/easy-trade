namespace EasyTrade.Service.Exceptions;

public class InvalidUserException : Exception
{
    public InvalidUserException() : base("Invalid userId in token")
    {
        
    }
}