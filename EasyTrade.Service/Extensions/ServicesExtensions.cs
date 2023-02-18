using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Repository;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Model.ResponseModels;
using EasyTrade.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace EasyTrade.Service.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddQuotesProvider(this IServiceCollection services, QuotesApiConfiguration config)
    {
        services.AddSingleton<IQuotesProvider, QuotesProvider>().AddRefitClient<IQuotesApi>()
            .ConfigureHttpClient(u =>
            {
                u.BaseAddress = new Uri(config.Url);
                u.DefaultRequestHeaders.Add("apikey", config.ApiKey);
            });
        
        return services;
    }

    public static IServiceCollection AddLocalCurrenciesProvider(this IServiceCollection services)
    {
        services.AddTransient<ITerminologyApi, TerminologyLocalApi>()
            .AddTransient<ICurrenciesProvider, CurrenciesProvider>();

        return services;
    }


}