using EasyTrade.Service.Abstractions;
using EasyTrade.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace EasyTrade.Service.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddQuotesProvider(this IServiceCollection services, string url)
    {
        services.AddSingleton<IQuotesProvider, QuotesProvider>().AddRefitClient<IQuotesApi>()
            .ConfigureHttpClient(u => u.BaseAddress = new Uri(url));
        
        return services;
    }
}