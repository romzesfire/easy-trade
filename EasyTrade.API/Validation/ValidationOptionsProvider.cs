using System.Net;
using EasyTrade.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.API.Validation;

public class ValidationOptionsProvider : IValidationOptionsProvider
{
    private Dictionary<Type, ValidationOptions> _options;

    public ValidationOptionsProvider()
    {
        _options = new Dictionary<Type, ValidationOptions>()
        {
            {
                typeof(CurrencyNotFoundException), 
                new ValidationOptions()
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                }
            },
            {
                typeof(NotEnoughAssetsException), 
                new ValidationOptions()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                }
            },
            {
                typeof(AccountCurrencyNotFoundException), 
                new ValidationOptions()
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                }
            },
            {
                typeof(ConcurrentWriteException), 
                new ValidationOptions()
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                }
            }
        };
    }

    public Dictionary<Type, ValidationOptions> Get() => _options;
}