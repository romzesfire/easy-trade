
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
    private ICoefficientProvider _coefficientProvider;
    private EasyTradeDbContext _db;
    private IDataSaver _saver;
    private IRepository<Currency, int> _currencyRepository;
    public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        ICoefficientProvider coefficientProvider, EasyTradeDbContext dbContext,
        IRepository<Currency, int> currencyRepository,
        IBalanceProvider balanceProvider, IDataSaver saver)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _coefficientProvider = coefficientProvider;
        _db = dbContext;
        _currencyRepository = currencyRepository;
        _saver = saver;
    }
    
    public void Create(TradeCreationModel tradeModel)
    {
        var currencies = _currencyRepository.GetAll();
        
        var buyCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.BuyCurrency);
        var sellCcy = currencies.FirstOrDefault(c => c.IsoCode == tradeModel.SellCurrency);
        
        ValidateCurrencies(buyCcy, sellCcy);
        var brokerTrade = _brokerTradeCreator.Create(tradeModel, buyCcy, sellCcy);
        
        var c = _coefficientProvider.GetCoefficient(TradeOperation.CurrencyTrade).Coefficient;

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
        _saver.Save();
    }



    private void ValidateCurrencies(Currency? buyCcy, Currency? sellCcy)
    {
        if (buyCcy == null)
        {
            throw new CurrencyNotFoundException(buyCcy.IsoCode);
        }

        if (sellCcy == null)
        {
            throw new CurrencyNotFoundException(sellCcy.IsoCode);
        }
    }
}