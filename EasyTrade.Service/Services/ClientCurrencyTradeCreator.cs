
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services;

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private EasyTradeDbContext _db;
    private IRepository<Currency, string> _currencyRepository;
    private IRepository<Balance, string> _balanceRepository;
    private IRepository<CurrencyTradeCoefficient, (string?, string?)> _coefficientRepository;
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
    
    public void Create(TradeCreationModel tradeModel)
    {
        var currencies = _currencyRepository.GetAll();
        
        var buyCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.BuyCurrency);
        var sellCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.SellCurrency);
        
        ValidateCurrencies(buyCcy, sellCcy, tradeModel);
        
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientRepository.Get((buyCcy.IsoCode, sellCcy.IsoCode)).Coefficient;

        var buyAmount = brokerTrade.BuyAmount;
        var sellAmount = brokerTrade.SellAmount;
        if (tradeModel is BuyTradeCreationModel buyModel)
        {
            sellAmount *= c;
        }

        if (tradeModel is SellTradeCreationModel sellModel)
        {
            buyAmount /= c;
        }
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
        
        var clientTrade = new ClientCurrencyTrade(brokerTrade, buyAmount, sellAmount);
        _db.Balances.AddRange(buyOperation, sellOperation);
        _db.AddTrade(clientTrade);
        _db.SaveChanges();
    }

    private void ValidateBalance(Currency sellCcy, decimal sellAmount)
    {
        var balance = _balanceRepository.Get(sellCcy.IsoCode);
        if (balance.Amount < sellAmount)
            throw new NotEnoughAssetsException(sellCcy.IsoCode);
    }


    private void ValidateCurrencies(Currency? buyCcy, Currency? sellCcy, TradeCreationModel creationModel)
    {
        if (buyCcy == null)
        {
            throw new CurrencyNotFoundException(creationModel.BuyCurrency);
        }

        if (sellCcy == null)
        {
            throw new CurrencyNotFoundException(creationModel.SellCurrency);
        }
    }
}