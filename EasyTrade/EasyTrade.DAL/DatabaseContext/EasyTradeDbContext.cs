using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EasyTrade.DAL.DatabaseContext;

public interface IEasyTradeDbContext
{
    public void AddTrade(ClientCurrencyTrade currencyTrade);
    public ClientCurrencyTrade GetTrade(uint id);
}

public class EasyTradeDbContext : DbContext, IEasyTradeDbContext
{
    private string _connectionString;
    private DbSet<ClientCurrencyTrade> _trades; 
    
    public EasyTradeDbContext(IOptions<DbConfigutation> config)
    {
        _connectionString = config.Value.ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        _trades.AddRange(currencyTrade);
        SaveChanges();
    }

    public ClientCurrencyTrade GetTrade(uint id)
    {
        var trade = _trades.FirstOrDefault(t => t.Id == id);
        return trade;
    }
}