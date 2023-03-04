using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTrade.Domain.Extensions;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainCalculationProvider, DomainCalculatorProvider>();
        return services;
    }
}