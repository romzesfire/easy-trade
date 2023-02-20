
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services;

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private readonly IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private readonly EasyTradeDbContext _db;
    private readonly IRepository<Currency, string> _currencyRepository;
    private readonly IRepository<Balance, string> _balanceRepository;
    private readonly IRepository<CurrencyTradeCoefficient, (string?, string?)> _coefficientRepository;
    public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        EasyTradeDbContext dbContext, IRepository<Currency, string> currencyRepository,
        IRepository<Balance, string> balanceRepository,
        IRepository<CurrencyTradeCoefficient, (string?, string?)> coefficientRepository)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _coefficientRepository = coefficientRepository;
        _db = dbContext;
        _currencyRepository = currencyRepository;
        _balanceRepository = balanceRepository;
    }

    public void Create(BuyTradeCreationModel tradeModel)
    {
        var (buyCcy, sellCcy) = GetCurrencies(tradeModel);
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientRepository.Get((buyCcy.IsoCode, sellCcy.IsoCode)).Coefficient;
        var buyAmount = brokerTrade.BuyAmount;
        var sellAmount = brokerTrade.SellAmount;
        sellAmount *= c;
        
        AddBalances(sellCcy, sellAmount, buyCcy, buyAmount, tradeModel);
        CreateClientTrade(brokerTrade, buyAmount, sellAmount);
    }
    
    public void Create(SellTradeCreationModel tradeModel)
    {
        (var buyCcy, var sellCcy) = GetCurrencies(tradeModel);
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientRepository.Get((buyCcy.IsoCode, sellCcy.IsoCode)).Coefficient;
        var buyAmount = brokerTrade.BuyAmount;
        var sellAmount = brokerTrade.SellAmount;
        buyAmount /= c;
        
        AddBalances(sellCcy, sellAmount, buyCcy, buyAmount, tradeModel);
        CreateClientTrade(brokerTrade, buyAmount, sellAmount);
    }

    private (Currency?, Currency?) GetCurrencies(TradeCreationModel tradeModel)
    {
       
        var buyCcy = _currencyRepository.Get(tradeModel.BuyCurrency);
        var sellCcy = _currencyRepository.Get(tradeModel.SellCurrency);

        return (buyCcy, sellCcy);
    }
    

    
    private void ValidateBalance(Currency sellCcy, decimal sellAmount)
    {
        var balance = _balanceRepository.Get(sellCcy.IsoCode);
        if (balance.Amount < sellAmount)
            throw new NotEnoughAssetsException(sellCcy.IsoCode);
    }
    
    private void AddBalances(Currency sellCcy, decimal sellAmount, Currency buyCcy, decimal buyAmount,
        TradeCreationModel tradeModel)
    {
        ValidateBalance(sellCcy, sellAmount);
        var sellOperation = new Balance()
        {
            Currency = sellCcy,
            Amount = (-1) * sellAmount,
            DateTime = tradeModel.DateTime
        };
        var buyOperation = new Balance()
        {
            Currency = buyCcy,
            Amount = buyAmount,
            DateTime = tradeModel.DateTime
        };
        _db.Balances.AddRange(buyOperation, sellOperation);
    }
    
    private void CreateClientTrade(BrokerCurrencyTrade brokerTrade, decimal buyAmount, decimal sellAmount)
    {
        var clientTrade = new ClientCurrencyTrade(brokerTrade, buyAmount, sellAmount);

        _db.AddTrade(clientTrade);
        _db.SaveChanges();
    }

}