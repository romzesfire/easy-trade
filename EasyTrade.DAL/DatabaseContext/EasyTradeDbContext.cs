using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
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
        var ccys = operations.Select(o => o.Currency).Distinct();
        foreach (var ccy in ccys)
        {
            AddOperation(operations.Where(o=>o.Currency.IsoCode == ccy.IsoCode), ccy);
        }
    }
    
    
    public void AddOperation(IEnumerable<Operation> operations, Currency ccy)
    {
        var balance = Balances.FirstOrDefault(b=>b.CurrencyIso == ccy.IsoCode);
        balance = Calculator.CalculateBalance(balance, operations, ccy);
        Operations.AddRange(operations);

        if (balance.Id == -1)
            Balances.AddRange(balance);
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