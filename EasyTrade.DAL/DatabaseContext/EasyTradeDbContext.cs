using EasyTrade.DAL.Configuration;
using EasyTrade.DTO.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EasyTrade.DAL.DatabaseContext;

public interface IEasyTradeDbContext
{
    public void AddTrade(ClientCurrencyTrade currencyTrade);
    public ClientCurrencyTrade GetTrade(uint id);
}

public class EasyTradeDbContext : DbContext
{
    private DbSet<ClientCurrencyTrade> _clientTrades { get; set; } = null!;
    private DbSet<BrokerCurrencyTrade> _brokerTrades { get; set; } = null!;
    private DbSet<Currency> _currencies { get; set; } = null!;
    public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) : base(options)
    {
        
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<ClientCurrencyTrade>().ToTable("client_trade");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.Id).HasColumnName("id");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.BuyAmount).HasColumnName("buyamount");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.SellAmount).HasColumnName("sellamount");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.BrokerCurrencyTradeId)
    //         .HasColumnName("brokercurrencytradeid");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.BuyCcyId)
    //         .HasColumnName("buyccyid");
    //     modelBuilder.Entity<ClientCurrencyTrade>().Property(p => p.SellCcyId)
    //         .HasColumnName("sellccyid");
    //
    //     modelBuilder.Entity<BrokerCurrencyTrade>().ToTable("broker_trade");
    //     modelBuilder.Entity<BrokerCurrencyTrade>().Property(p => p.Id).HasColumnName("id");
    //     modelBuilder.Entity<BrokerCurrencyTrade>().Property(p => p.BuyAmount).HasColumnName("buyamount");
    //     modelBuilder.Entity<BrokerCurrencyTrade>().Property(p => p.SellAmount).HasColumnName("sellamount");
    //     modelBuilder.Entity<BrokerCurrencyTrade>().Property(p => p.BuyCcyId)
    //         .HasColumnName("buyccyid");
    //     modelBuilder.Entity<BrokerCurrencyTrade>().Property(p => p.SellCcyId)
    //         .HasColumnName("sellccyid");
    //     
    //      modelBuilder.Entity<Currency>().ToTable("currency");
    //     modelBuilder.Entity<Currency>().Property(p => p.Id).HasColumnName("id");
    //     modelBuilder.Entity<Currency>().Property(p => p.Name).HasColumnName("name");
    //     modelBuilder.Entity<Currency>().Property(p => p.IsoCode).HasColumnName("isocode");
    //
    // }

    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        _clientTrades.Add(currencyTrade);
        SaveChanges();
    }
    public IEnumerable<Currency> GetCurrencies()
    {
        return _currencies.ToList();
    }

    public List<ClientCurrencyTrade> GetTrades(int limit, int offset)
    {
        return _clientTrades.Skip(offset).Take(limit).Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).ToList();
    }

    public Trade GetTrade(uint id)
    {
        return _clientTrades.Where(t => t.Id == id).Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).First();
    }
}