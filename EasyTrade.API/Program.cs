using System.Text.Json;
using System.Text.Json.Serialization;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
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

var options = optionsBuilder.UseNpgsql(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString);
var dd = options.Options;


builder.Services.AddQuotesProvider(configuration.GetSection("ApiLayer").Get<QuotesApiConfiguration>())
    .AddLocalCurrenciesProvider()
    .AddScoped<IBalanceProvider, BalanceDbProvider>()
    .AddScoped<ITradesProvider, TradesDbProvider>()
    .AddScoped<IDataSaver, DataToDbSaver>()
    .AddScoped<ICurrenciesProvider, CurrenciesProvider>()
    .AddDbServices(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString)
    .AddScoped<IBrokerCurrencyTradeCreator, BrokerCurrencyTradeCreator>()
    .AddScoped<IClientCurrencyTradeCreator, ClientCurrencyTradeCreator>()
    .AddScoped<ICoefficientProvider, CoefficientsDbProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();