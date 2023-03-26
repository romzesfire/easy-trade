using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;

namespace EasyTrade.Test.Extension;

public static class TestExtensions
{
    public static void GenerateInMemoryData(this EasyTradeDbContext dbContext)
    {
        var usd = new Currency(1, "Dollar", "USD");
        var rub = new Currency(2, "Rouble", "RUB");
        var eur = new Currency(3, "Euro", "EUR");
        dbContext.Currencies.AddRange(
            new []
            {
                usd,
                rub,
                eur
            });
        
        dbContext.Balances.AddRange(new []
            {
                new Balance() { Amount = 1000000, Currency = usd, Id = 1, Version = new Guid() },
                new Balance() { Amount = 1000000, Currency = rub, Id = 2, Version = new Guid() },
                new Balance() { Amount = 1000000, Currency = eur, Id = 3, Version = new Guid() }
            });
        
        dbContext.Operations.AddRange(
            new []
            {
                new Operation() { Amount = 1000000, Currency = usd, Id = 1, DateTime = DateTimeOffset.Now },
                new Operation() { Amount = 1000000, Currency = rub, Id = 2, DateTime = DateTimeOffset.Now },
                new Operation() { Amount = 1000000, Currency = eur, Id = 3, DateTime = DateTimeOffset.Now }
            });
        
        dbContext.Coefficients.AddRange(new[]
        {
            new CurrencyTradeCoefficient() { Coefficient = 1.25M, DateTime = DateTimeOffset.Now, Id = 1, FirstCcy = null, 
                SecondCcy = null },
            new CurrencyTradeCoefficient() { Coefficient = 2M, DateTime = DateTimeOffset.Now, Id = 2, FirstCcy = usd, 
                SecondCcy = rub }
        });
        dbContext.SaveChanges();
    }
}