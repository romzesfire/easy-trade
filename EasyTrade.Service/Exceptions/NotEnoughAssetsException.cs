namespace EasyTrade.Service.Exceptions;

public class NotEnoughAssetsException : Exception
{
    public NotEnoughAssetsException(string asset) : base($"Not enough {asset} for this operation")
    { }
}