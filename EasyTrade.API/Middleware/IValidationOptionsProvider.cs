namespace EasyTrade.API.Middleware;

public interface IValidationOptionsProvider
{
    Dictionary<Type, ValidationOptions> Get();
}