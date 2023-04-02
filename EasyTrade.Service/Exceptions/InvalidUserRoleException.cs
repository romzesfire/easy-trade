namespace EasyTrade.Service.Exceptions;

public class InvalidUserRoleException : Exception
{
    public InvalidUserRoleException() : base("Invalid user role in token")
    {
        
    }
}