namespace EasyTrade.Service.Exceptions;

public class ConcurrentWriteException : Exception
{
    public ConcurrentWriteException() : base($"Failed to save. Please, try again.")
    {
        
    }
}