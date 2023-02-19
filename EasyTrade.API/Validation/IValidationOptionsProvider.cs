namespace EasyTrade.API.Validation;

public interface IValidationOptionsProvider
{
    Dictionary<Type, ValidationOptions> Get();
}