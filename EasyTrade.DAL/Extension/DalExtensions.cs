using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DAL.Repository;
using EasyTrade.DTO.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTrade.DAL.Extension;

public static class DalExtensions
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<EasyTradeDbContext>(o =>
        {
            o.UseNpgsql(connectionString);
        })
            .AddScoped<IRepository<Balance, string>, BalanceRepository>()
            .AddScoped<IRepository<Currency, string>, CurrencyRepository>()
            .AddScoped<IRepository<CurrencyTradeCoefficient, (string?, string?)>,
                CurrencyTradeCoefficientRepository>()
            .AddScoped<IRepository<CurrencyTrade, int>, CurrencyTradeRepository>();
        
        return services;
    }
}