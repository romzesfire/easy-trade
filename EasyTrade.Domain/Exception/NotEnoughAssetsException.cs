namespace EasyTrade.Domain.Exception;

public class NotEnoughAssetsException : System.Exception
{
    public NotEnoughAssetsException(string asset) : base($"Not enough {asset} for this operation")
    { }
}