
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services;

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private ICoefficientProvider _coefficientProvider;
    private EasyTradeDbContext _db;
     public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        ICoefficientProvider coefficientProvider, EasyTradeDbContext dbContext)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _coefficientProvider = coefficientProvider;
        _db = dbContext;
    }
    
    public void Create(TradeCreationModel tradeModel)
    {
        var currencies = _db.GetCurrencies();
        (var buyCcy, var sellCcy) = ValidateCurrencies(tradeModel);
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientProvider.GetCoefficient();

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
        var clientTrade = new ClientCurrencyTrade(brokerTrade, buyAmount, sellAmount);

        _db.AddTrade(clientTrade);
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