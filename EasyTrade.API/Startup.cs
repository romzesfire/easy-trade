using System.Text.Json.Serialization;
using EasyTrade.API.Configuration;
using EasyTrade.API.Extension;
using EasyTrade.API.Validation;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Extensions;
using EasyTrade.Repositories.Extensions;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Extensions;
using EasyTrade.Service.Services;
using EasyTrade.Service.Services.Cache;
using EasyTrade.Service.Services.Recorder;
using EasyTrade.Service.Services.Security;
using EasyTradeLibs.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        _builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header, 
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey 
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" 
                        } 
                    },
                    new string[] { } 
                } 
            });
        });
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

        _builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });
        _builder.Services.AddQuotesProvider(_configuration.GetSection("ApiLayer").Get<QuotesApiConfiguration>())
            .AddValidationOptions()
            .AddDomainServices()
            .AddRepositories()
            .AddMemoryCache()
            .AddLocalCurrenciesProvider()
            .AddSingleton<IClaimsExecutor, ClaimsExecutor>()
            .AddSingleton<ICacheServiceFactory, CacheServiceFactory>()
            .AddScoped<IOperationProvider, OperationDbProvider>()
            .Configure<LockerConfiguration>(_configuration.GetSection("Locker"))
            .AddScoped<ICurrencyTradesProvider, CurrencyTradesDbProvider>()
            .AddScoped<ICurrenciesProvider, CurrenciesProvider>()
            .AddScoped<IBalanceProvider, BalanceDbProvider>()
            .AddSingleton<ISecurityKeyValidator, SecurityKeyValidator>()
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
        _app.UseRouting();
        _app.UseAuthentication();
        _app.UseAuthorization();

        _app.MapControllers();
        _app.UseMiddleware<ExceptionMiddleware>();
    }

    public void Run()
    {
        _app.Run();
    }
}