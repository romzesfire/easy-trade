using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Repositories.Repository;
using EasyTrade.Service.Model.ResponseModels;
using EasyTrade.Service.Services;
using EasyTrade.Service.Services.Cache;
using EasyTrade.Service.Services.Recorder;
using EasyTrade.Test.Extension;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EasyTrade.Test.Model;

public class ClientCurrencyTradeCreatorModel
{
    public Mock<IQuotesProvider> QuotesProvider { get;}
    public IBrokerCurrencyTradeCreator BrokerTradeCreator { get; }
    public EasyTradeDbContext Db { get; }
    public ICurrencyTradeCoefficientsProvider CoefficientProvider { get; }
    public Mock<ILocker> Locker { get; }
    public IOperationRecorder OperationRecorder { get; }
    public IPriceMarkupCalculator? PriceMarkupCalculator { get; }
    public IRepository<Currency, string> CurrencyRepository { get; }
    
    public ClientCurrencyTradeCreatorModel(TradeCreationModel creationModel, decimal price)
    {
        QuotesProvider = new Mock<IQuotesProvider>();
        QuotesProvider.Setup(q => q.Get(creationModel.SellCurrency, creationModel.BuyCurrency))
            .ReturnsAsync(new Quote(
                new QuoteResponse()
                {
                    Query = new Query() { From = creationModel.SellCurrency, To = creationModel.BuyCurrency },
                    Result = price
                }));
        Locker = new Mock<ILocker>();
        Locker.Setup(l => l.ConcurrentExecuteAsync(It.IsAny<Action>(), It.IsAny<object>()))
            .Callback<Action, object>((action, o) => action());

        PriceMarkupCalculator = new PriceMarkupCalculator();
        Db = CreateDbContext();
        CurrencyRepository = new CurrencyRepository(Db);
        OperationRecorder = new OperationRecorder(Db, CurrencyRepository, Locker.Object,
            new DomainCalculatorProvider(), new BalanceRepository(Db));
        BrokerTradeCreator = new BrokerCurrencyTradeCreator(QuotesProvider.Object,
            new DomainCalculatorProvider());
        var coefficientRepository = new CurrencyTradeCoefficientRepository(Db);
        CoefficientProvider = new CurrencyTradeCoefficientsProvider(coefficientRepository, new CacheServiceFactory());
        CurrencyRepository = new CurrencyRepository(Db);
    }
    private EasyTradeDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<EasyTradeDbContext>()
            .UseInMemoryDatabase("EasyTradeDb").Options;
        var dbContext = new EasyTradeDbContext(options);
        dbContext.GenerateInMemoryData();
        return dbContext;
    }
}

public class ClientCurrencyBuyTradeCreatorModel : ClientCurrencyTradeCreatorModel
{
    public decimal ExpectedSellAmount { get; }
    public BuyTradeCreationModel BuyTradeCreationModel;
    public ClientCurrencyBuyTradeCreatorModel(BuyTradeCreationModel creationModel, decimal price, 
        decimal expectedSellAmount) : base(creationModel, price)
    {
        ExpectedSellAmount = expectedSellAmount;
        BuyTradeCreationModel = creationModel;
    }
}

public class ClientCurrencySellTradeCreatorModel : ClientCurrencyTradeCreatorModel
{
    public decimal ExpectedBuyAmount { get; }
    public SellTradeCreationModel SellTradeCreationModel;
    public ClientCurrencySellTradeCreatorModel(SellTradeCreationModel creationModel, decimal price,
        decimal expectedBuyAmount) : base(creationModel, price)
    {
        ExpectedBuyAmount = expectedBuyAmount;
        SellTradeCreationModel = creationModel;
    }
}