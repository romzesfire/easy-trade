using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BrokerCurrencyTrade = EasyTrade.DTO.Model.BrokerCurrencyTrade;
using ClientCurrencyTrade = EasyTrade.DTO.Model.ClientCurrencyTrade;
using Currency = EasyTrade.DTO.Model.Currency;
using CurrencyTrade = EasyTrade.DTO.Model.CurrencyTrade;

namespace EasyTrade.DAL.DatabaseContext;

public interface IEasyTradeDbContext
{
    public void AddTrade(ClientCurrencyTrade currencyTrade);
    public ClientCurrencyTrade GetTrade(uint id);
}

public class EasyTradeDbContext : DbContext
{
    private DbSet<ClientCurrencyTrade> clientTrades { get; set; } = null!;
    private DbSet<BrokerCurrencyTrade> brokerTrades { get; set; } = null!;
    private DbSet<Currency> currencies { get; set; } = null!;
    private DbSet<TradeCoefficient> coefficients { get; set; } = null!;
    private DbSet<Balance> balances { get; set; } = null!;
    public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) : base(options)
    {
        
    }



    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        clientTrades.Add(currencyTrade);
        SaveChanges();
    }
    public IEnumerable<Currency> GetCurrencies()
    {
        return currencies.ToList();
    }

    public List<ClientCurrencyTrade> GetTrades(int limit, int offset)
    {
        return clientTrades.Skip(offset).Take(limit).Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).ToList();
    }

    public ClientCurrencyTrade GetTrade(uint id)
    {
        return clientTrades.Where(t => t.Id == id)
            .Include(t => t.BrokerCurrencyTrade)
            .Include(t => t.BuyCcy)
            .Include(t => t.SellCcy).First();
    }

    public IEnumerable<TradeCoefficient> GetCoefficients()
    {
        return coefficients.ToList();
    }

    public IEnumerable<Balance> GetBalances(int limit, int offset)
    {
        return balances.Skip(offset).Take(limit).Include(b => b.Currency).ToList();
    }
    
    public Balance GetBalance(uint id)
    {
        return balances.Include(b => b.Currency).First(b => b.Id == id);
    }
    
    public Balance GetBalance(string iso)
    {
        return balances.Include(b => b.Currency).First(b => b.Currency.IsoCode == iso);
    }
}