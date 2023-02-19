using EasyTrade.API.Middleware;

namespace EasyTrade.API.Extension;

public static class ApiExtensions
{
    public static IServiceCollection AddValidationOptions(this IServiceCollection services)
    {
        services.AddSingleton<IValidationOptionsProvider, ValidationOptionsProvider>();
        return services;
    } 
}