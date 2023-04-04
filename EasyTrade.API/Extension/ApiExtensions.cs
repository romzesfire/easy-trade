using EasyTrade.API.Validation;
using EasyTradeLibs.Abstractions;

namespace EasyTrade.API.Extension;

public static class ApiExtensions
{
    public static IServiceCollection AddValidationOptions(this IServiceCollection services)
    {
        services.AddSingleton<IValidationOptionsProvider, ValidationOptionsProvider>();
        return services;
    } 
}