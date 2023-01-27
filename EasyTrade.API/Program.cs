using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Extensions;
using EasyTrade.Service.Services;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var optionsBuilder = new DbContextOptionsBuilder<EasyTradeDbContext>();
var x = configuration.GetSection("Database").Get<DbConfigutation>();
var options = optionsBuilder.UseNpgsql(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString);
var dd = options.Options;

builder.Services.AddDbContextPool<EasyTradeDbContext>(o =>
{
    o.UseNpgsql(configuration.GetSection("Database").Get<DbConfigutation>().ConnectionString);
});

builder.Services.AddQuotesProvider(configuration.GetSection("ApiLayer").Get<QuotesApiConfiguration>())
    .AddLocalCurrenciesProvider()
    .AddSingleton<IBrokerCurrencyTradeCreator, BrokerCurrencyTradeCreator>()
    .AddSingleton<IClientCurrencyTradeCreator, ClientCurrencyTradeCreator>()
    .AddSingleton<ICoefficientProvider, CoefficientProvider>();
    






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