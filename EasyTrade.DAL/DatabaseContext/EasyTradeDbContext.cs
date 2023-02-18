using System.ComponentModel.DataAnnotations.Schema;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyTrade.DAL.DatabaseContext;

public interface IEasyTradeDbContext
{
    public void AddTrade(ClientCurrencyTrade currencyTrade);
    public ClientCurrencyTrade GetTrade(uint id);
}

public class EasyTradeDbContext : DbContext
{
    public DbSet<ClientCurrencyTrade> ClientTrades { get; set; } = null!;

    public DbSet<BrokerCurrencyTrade> BrokerTrades { get; set; } = null!;

    public DbSet<Currency> Currencies { get; set; } = null!;

    public DbSet<CurrencyTradeCoefficient> Coefficients { get; set; } = null!;

    public DbSet<Balance> Balances { get; set; } = null!;

    public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) : base(options)
    {

    }


    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        ClientTrades.Add(currencyTrade);
    }
}