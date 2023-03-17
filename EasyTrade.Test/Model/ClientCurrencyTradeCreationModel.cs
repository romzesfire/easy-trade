using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Repositories.Repository;
using EasyTrade.Service.Services;
using EasyTrade.Service.Services.Recorder;
using Moq;

namespace EasyTrade.Test.Model;

public class ClientCurrencyTradeCreatorModel
{
    public IBrokerCurrencyTradeCreator BrokerTradeCreator { get; }
    public EasyTradeDbContext Db { get; }
    public IRepository<Currency, string> CurrencyRepository { get; }
    public IRepository<CurrencyTradeCoefficient, (string?, string?)> CoefficientRepository { get; }
    public Mock<ILocker> Locker { get; }
    public IOperationRecorder OperationRecorder { get; }
    public IPriceMarkupCalculator? PriceMarkupCalculator { get; }
    
    public ClientCurrencyTradeCreatorModel(EasyTradeDbContext dbContext, 
        BrokerCurrencyTradeCreatorModel brokerModel)
    {
        Locker = new Mock<ILocker>();
        Locker.Setup(l => l.ConcurrentExecuteAsync(It.IsAny<Action>(), It.IsAny<object>()))
            .Callback<Action, object>((action, o) => action());
        OperationRecorder = new OperationRecorder(dbContext, CurrencyRepository, Locker.Object,
            new DomainCalculatorProvider());

        PriceMarkupCalculator = new PriceMarkupCalculator();
        Db = dbContext;
        BrokerTradeCreator = new BrokerCurrencyTradeCreator(brokerModel.QuotesProvider.Object,
            new DomainCalculatorProvider());
        CoefficientRepository = new CurrencyTradeCoefficientRepository(dbContext);
        CurrencyRepository = new CurrencyRepository(dbContext);
    }
}

public class ClientCurrencyBuyTradeCreatorModel : ClientCurrencyTradeCreatorModel
{
    public decimal ExpectedSellAmount { get; }
    public ClientCurrencyBuyTradeCreatorModel(EasyTradeDbContext dbContext, 
        BrokerCurrencyBuyTradeCreatorModel brokerModel, decimal expectedSellAmount) : base(dbContext, brokerModel)
    {
        ExpectedSellAmount = expectedSellAmount;
    }
}

public class ClientCurrencySellTradeCreatorModel : ClientCurrencyTradeCreatorModel
{
    public decimal ExpectedBuyAmount { get; }
    public ClientCurrencySellTradeCreatorModel(EasyTradeDbContext dbContext, 
        BrokerCurrencySellTradeCreatorModel brokerModel, decimal expectedBuyAmount) : base(dbContext, brokerModel)
    {
        ExpectedBuyAmount = expectedBuyAmount;
    }
}