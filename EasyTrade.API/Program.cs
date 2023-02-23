using System.Text.Json.Serialization;
using EasyTrade.API.Configuration;
using EasyTrade.API.Extension;
using EasyTrade.API.Validation;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Extensions;
using EasyTrade.Service.Services;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = configuration.GetConnectionString("Database");
var optionsBuilder = new DbContextOptionsBuilder<EasyTradeDbContext>();
var lockerCfg = configuration.Get<LockerConfiguration>();
var options = optionsBuilder.UseNpgsql(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString);
var dd = options.Options;
if (lockerCfg.Type == LockerType.Optimistic)
{
    builder.Services.AddSingleton<ILocker, OptimisticLocker>();
}
else if(lockerCfg.Type == LockerType.Pessimistic)
{
    builder.Services.AddSingleton<ILocker, PessimisticLocker>();
}

builder.Services.AddQuotesProvider(configuration.GetSection("ApiLayer").Get<QuotesApiConfiguration>())
    .AddValidationOptions()
    .AddLocalCurrenciesProvider()
    .AddScoped<IOperationProvider, OperationDbProvider>()
    .Configure<LockerConfiguration>(configuration.GetSection("Locker"))
    .AddScoped<ICurrencyTradesProvider, CurrencyTradesDbProvider>()
    .AddScoped<ICurrenciesProvider, CurrenciesProvider>()
    .AddScoped<IBalanceProvider, BalanceDbProvider>()
    .AddDbServices(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString)
    .AddScoped<IBrokerCurrencyTradeCreator, BrokerCurrencyTradeCreator>()
    .AddScoped<IClientCurrencyTradeCreator, ClientCurrencyTradeCreator>()
    .AddScoped<ICurrencyTradeCoefficientsProvider, CurrencyTradeCoefficientsProvider>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();