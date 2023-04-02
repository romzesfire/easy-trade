
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private readonly IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _currencyRepository;
    //private readonly IRepository<CurrencyTradeCoefficient, (string?, string?)> _coefficientRepository;
    private readonly ILocker _locker;
    private readonly IOperationRecorder _operationRecorder;
    private readonly IPriceMarkupCalculator? _priceMarkupCalculator;
    private readonly ICurrencyTradeCoefficientsProvider _coefficientsProvider;
    
    public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        ILocker locker, EasyTradeDbContext dbContext, IRepository<Currency, string> currencyRepository,
        //IRepository<CurrencyTradeCoefficient, (string?, string?)> coefficientRepository,
        ICurrencyTradeCoefficientsProvider coefficientsProvider,
        IOperationRecorder operationRecorder, IDomainCalculationProvider calculationProvider)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _priceMarkupCalculator = calculationProvider.Get<IPriceMarkupCalculator>();
        _coefficientsProvider = coefficientsProvider;
        _db = dbContext;
        _currencyRepository = currencyRepository;
        _locker = locker;
        _operationRecorder = operationRecorder;
    }

    public async Task Create(BuyTradeCreationModel tradeModel, Guid userId)
    {
        var (buyCcy, sellCcy) = await GetCurrencies(tradeModel);
        var brokerTrade = await _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var coefficientModel = await _coefficientsProvider.GetCoefficient(buyCcy.IsoCode, sellCcy.IsoCode);
        var c = coefficientModel.Coefficient;
        var buyAmount = brokerTrade.BuyAmount;
        var sellAmount = brokerTrade.SellAmount;
        sellAmount = _priceMarkupCalculator.CalculateSellAmount(sellAmount, c);
        
        var clientTrade = CreateClientTrade(brokerTrade, userId, buyAmount, sellAmount);
        await _locker.ConcurrentExecuteAsync(() =>
            {
                AddBalances(clientTrade, userId);
                _db.AddTrade(clientTrade);
                _db.SaveChangesAsync();
            },
            sellCcy
        );
    }
    
    public async Task Create(SellTradeCreationModel tradeModel, Guid userId)
    {
        (var buyCcy, var sellCcy) = await GetCurrencies(tradeModel);
        var brokerTrade = await _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var coefficientModel = await _coefficientsProvider.GetCoefficient(buyCcy.IsoCode, sellCcy.IsoCode);
        var c = coefficientModel.Coefficient;
        var buyAmount = brokerTrade.BuyAmount;
        var sellAmount = brokerTrade.SellAmount;
        buyAmount /= c;

        var clientTrade = CreateClientTrade(brokerTrade, userId, buyAmount, sellAmount);
        await _locker.ConcurrentExecuteAsync(() =>
             {
                 AddBalances(clientTrade, userId);
                 _db.AddTrade(clientTrade);
                 _db.SaveChanges();
             },
             sellCcy
         );
    }

    private async Task<(Currency?, Currency?)> GetCurrencies(TradeCreationModel tradeModel)
    {
        var buyCcy = await _currencyRepository.Get(tradeModel.BuyCurrency);
        var sellCcy = await _currencyRepository.Get(tradeModel.SellCurrency);

        return (buyCcy, sellCcy);
    }

    private void AddBalances(ClientCurrencyTrade tradeModel, Guid userId)
    {
        var sellOperation = new Operation()
        {
            Currency = tradeModel.SellCcy,
            Amount = (-1) * tradeModel.SellAmount,
            DateTime = tradeModel.DateTime,
            AccountId = userId
        };
        var buyOperation = new Operation()
        {
            Currency = tradeModel.BuyCcy,
            Amount = tradeModel.BuyAmount,
            DateTime = tradeModel.DateTime,
            AccountId = userId
        };
        _operationRecorder.Record(new [] { buyOperation, sellOperation }, userId);
    }
    
    private ClientCurrencyTrade CreateClientTrade(BrokerCurrencyTrade brokerTrade, Guid userId, decimal buyAmount, decimal sellAmount)
    {
        return new ClientCurrencyTrade(brokerTrade, userId, buyAmount, sellAmount);
    }

}