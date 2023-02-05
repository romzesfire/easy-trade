using EasyTrade.DAL.Configuration;
using EasyTrade.DTO.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BrokerCurrencyTrade = EasyTrade.DAL.Model.BrokerCurrencyTrade;
using ClientCurrencyTrade = EasyTrade.DAL.Model.ClientCurrencyTrade;

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

    public ClientCurrencyTrade AddTrade(ClientCurrencyTrade currencyTrade)
    {
        var brokerTrade = currencyTrade.BrokerCurrencyTrade;
        currencyTrade.BrokerCurrencyTrade = null;
        brokerTrade.BuyCcy = null;
        brokerTrade.SellCcy = null;
        currencyTrade.BuyCcy = null;
        currencyTrade.SellCcy = null;
        _brokerTrades.AddRange(brokerTrade);
        SaveChanges();
        currencyTrade.BrokerCurrencyTradeId = brokerTrade.Id;
        _clientTrades.AddRange(currencyTrade);
        SaveChanges();
        return currencyTrade;
    }
    public IEnumerable<Currency> GetCurrencies()
    {
        return _currencies.ToList();
    }

    public List<ClientCurrencyTrade> GetTrades()
    {
        return _clientTrades.Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).ToList();
    }
}