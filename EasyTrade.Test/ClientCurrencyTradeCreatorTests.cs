using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Model;
using EasyTrade.Service.Services;
using NUnit.Framework;

namespace EasyTrade.Test;

[TestFixture]
public class ClientCurrencyTradeCreatorTests
{
    private EasyTradeDbContext _dbContext;
    [SetUp]
    public void AddCasesData()
    {
        _dbContext = Effort.ObjectContextFactory.CreatePersistent<EasyTradeDbContext>("");

    }

    [Test]
    public void TestClientTradeCreator()
    {
        var creator = new ClientCurrencyTradeCreator();
    }
}