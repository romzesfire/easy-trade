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
    internal DbSet<ClientCurrencyTrade> clientTrades { get; set; } = null!;
    internal DbSet<BrokerCurrencyTrade> brokerTrades { get; set; } = null!;
    internal DbSet<Currency> currencies { get; set; } = null!;
    internal DbSet<CurrencyTradeCoefficient> coefficients { get; set; } = null!;
    internal DbSet<Balance> balances { get; set; } = null!;
    public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) : base(options)
    {
        
    }
    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        clientTrades.Add(currencyTrade);
    }

}