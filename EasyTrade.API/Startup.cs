using System.Text.Json.Serialization;
using EasyTrade.API.Extension;
using EasyTrade.API.Validation;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Extensions;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Extensions;
using EasyTrade.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.API;

public class Startup
{
    private IConfiguration _configuration;
    private WebApplicationBuilder? _builder;
    private WebApplication _app;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void CreateBuilder(params string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
    }

    public void AddServices()
    {
        _builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();
        string connectionString = _configuration.GetConnectionString("Database");
        var optionsBuilder = new DbContextOptionsBuilder<EasyTradeDbContext>();
        var lockerCfg = _configuration.GetSection("Locker").Get<LockerConfiguration>();
        var options =
            optionsBuilder.UseNpgsql(_configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString);
        var dd = options.Options;
        if (lockerCfg.Type == LockerType.Optimistic)
        {
            _builder.Services.AddSingleton<ILocker, OptimisticLocker>();
        }
        else if (lockerCfg.Type == LockerType.Pessimistic)
        {
            _builder.Services.AddSingleton<ILocker, PessimisticLocker>();
        }

        _builder.Services.AddQuotesProvider(_configuration.GetSection("ApiLayer").Get<QuotesApiConfiguration>())
            .AddValidationOptions()
            .AddDomainServices()
            .AddLocalCurrenciesProvider()
            .AddScoped<IOperationProvider, OperationDbProvider>()
            .Configure<LockerConfiguration>(_configuration.GetSection("Locker"))
            .AddScoped<ICurrencyTradesProvider, CurrencyTradesDbProvider>()
            .AddScoped<ICurrenciesProvider, CurrenciesProvider>()
            .AddScoped<IBalanceProvider, BalanceDbProvider>()
            .AddDbServices(_configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString)
            .AddScoped<IBrokerCurrencyTradeCreator, BrokerCurrencyTradeCreator>()
            .AddScoped<IClientCurrencyTradeCreator, ClientCurrencyTradeCreator>()
            .AddScoped<ICurrencyTradeCoefficientsProvider, CurrencyTradeCoefficientsProvider>();
    }

    public void Build()
    {
        _app = _builder.Build();
    }

    public void AddMiddleware()
    {
        if (_app.Environment.IsDevelopment())
        {
            _app.UseSwagger();
            _app.UseSwaggerUI();
        }

        _app.UseMiddleware<RequestDurationMiddleware>();
        _app.UseHttpsRedirection();

        _app.UseAuthorization();

        _app.MapControllers();
        _app.UseMiddleware<ExceptionMiddleware>();
    }

    public void Run()
    {
        _app.Run();
    }
}