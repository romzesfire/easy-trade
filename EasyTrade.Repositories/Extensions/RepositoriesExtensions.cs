using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Repositories.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTrade.Repositories.Extensions;

public static class RepositoriessExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IRepository<Operation, int>, OperationsRepository>()
            .AddScoped<IRepository<Currency, string>, CurrencyRepository>()
            .AddScoped<IRepository<Balance, string>, BalanceRepository>()
            .AddScoped<IRepository<CurrencyTradeCoefficient, (string?, string?)>,
                CurrencyTradeCoefficientRepository>()
            .AddScoped<IRepository<ClientCurrencyTrade, int>, CurrencyTradeRepository>();
        return services;
    }
}