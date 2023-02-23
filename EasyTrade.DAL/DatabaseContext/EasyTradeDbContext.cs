using EasyTrade.DAL.Model;
using EasyTrade.Service.Exceptions;
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

    public DbSet<Operation> Operations { get; set; } = null!;

    public DbSet<Balance> Balances { get; set; } = null!;
    public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) : base(options)
    {

    }

    public void AddTrade(ClientCurrencyTrade currencyTrade)
    {
        ClientTrades.Add(currencyTrade);
    }

    public void AddOperations(params Operation[] operations)
    {
        foreach (var operation in operations)
        {
            AddOperation(operation);
        }
    }
    
    public void AddOperation(Operation operation)
    {
        var sum = GetSumOfOperations(operation.Currency);
        sum += operation.Amount;
        if (sum < 0)
        {
            throw new NotEnoughAssetsException(operation.Currency.IsoCode);
        }
        Operations.Add(operation);
        var balance = Balances.Include(b=>b.Currency)
            .FirstOrDefault(b=>b.Currency.IsoCode == operation.Currency.IsoCode);
        
        if (balance == null)
        {
            throw new AccountCurrencyNotFoundException(operation.Currency.IsoCode);
        }
        balance.Amount = sum;
    }

    internal decimal GetSumOfOperations(Currency ccy)
    {
        var operations = Operations.Include(b => b.Currency)
            .Where(b => b.Currency.IsoCode == ccy.IsoCode).ToList();
            
        var sum = operations.Sum(o=>o.Amount);
        return sum;
         
    }


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     var rubCcy = new Currency() { Id = 1, IsoCode = "RUB", Name = "Russian Rouble" };
    //     var usdCcy = new Currency() { Id = 2, IsoCode = "USD", Name = "Dollar USA" };
    //     var gbpCcy = new Currency() { Id = 3, IsoCode = "GBP", Name = "Pound sterling" };
    //     var eurCcy = new Currency() { Id = 4, IsoCode = "EUR", Name = "Euro" };
    //     var jpyCcy = new Currency() { Id = 5, IsoCode = "JPY", Name = "Japanese Yen" };
    //     var audCcy = new Currency() { Id = 6, IsoCode = "AUD", Name = "Australian dollar" };
    //     var cadCcy = new Currency() { Id = 7, IsoCode = "CAD", Name = "Dollar canadien" };
    //     var chfCcy = new Currency() { Id = 8, IsoCode = "CHF", Name = "Swiss franc" };
    //     var cnhCcy = new Currency() { Id = 9, IsoCode = "CNH", Name = "Yuan" };
    //
    //     modelBuilder.Entity<Currency>().HasData(rubCcy, usdCcy, gbpCcy, eurCcy, jpyCcy, audCcy, cadCcy, chfCcy, cnhCcy);
    // }
}