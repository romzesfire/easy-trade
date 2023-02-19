namespace EasyTrade.Service.Exceptions;

public class NotEnoughAssetsException : BadRequestException
{
    public NotEnoughAssetsException(string asset) : base($"Not enough {asset} for this operation")
    { }
}