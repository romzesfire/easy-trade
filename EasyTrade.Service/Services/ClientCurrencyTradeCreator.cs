
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Exceptions;
using ClientCurrencyTrade = EasyTrade.DTO.Model.ClientCurrencyTrade;
using Currency = EasyTrade.DTO.Model.Currency;

namespace EasyTrade.Service.Services;

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private ICoefficientProvider _coefficientProvider;
    private EasyTradeDbContext _db;
    private IBalanceProvider _balanceProvider;
    private IDataSaver _saver;

    public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        ICoefficientProvider coefficientProvider, EasyTradeDbContext dbContext,
        IBalanceProvider balanceProvider, IDataSaver saver)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _coefficientProvider = coefficientProvider;
        _db = dbContext;
        _balanceProvider = balanceProvider;
        _saver = saver;
    }
    
    public void Create(TradeCreationModel tradeModel)
    {
        var currencies = _db.GetCurrencies();
        (var buyCcy, var sellCcy) = ValidateCurrencies(tradeModel);
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientProvider.GetCoefficient(TradeOperation.CurrencyTrade);

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

        var buyCurrencyBalance = _balanceProvider.GetBalance(brokerTrade.BuyCcy.IsoCode);
        var sellCurrencyBalance = _balanceProvider.GetBalance(brokerTrade.SellCcy.IsoCode);
        
        CalculateBalances(buyCurrencyBalance, sellCurrencyBalance, buyAmount, sellAmount);
        var clientTrade = new ClientCurrencyTrade(brokerTrade, buyAmount, sellAmount);

        _db.AddTrade(clientTrade);
        _saver.Save();
    }

    private void CalculateBalances(Balance buyBalance, Balance sellBalance, decimal buyAmount, decimal sellAmount)
    {
        if (sellAmount > sellBalance.Amount)
            throw new NotEnoughAssetsException(sellBalance.Currency.IsoCode);
        
        buyBalance.Amount += buyAmount;
        sellBalance.Amount -= sellAmount;
    }

    private (Currency, Currency) ValidateCurrencies(TradeCreationModel tradeModel)
    {
        var currencies = _db.GetCurrencies();
        var buyCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.BuyCurrency);
        var sellCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.SellCurrency);
        
        if (buyCcy == null)
        {
            throw new CurrencyNotFoundException(tradeModel.BuyCurrency);
        }

        if (sellCcy == null)
        {
            throw new CurrencyNotFoundException(tradeModel.SellCurrency);
        }

        return (buyCcy, sellCcy);
    }
}