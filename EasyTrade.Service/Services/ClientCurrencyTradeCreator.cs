using EasyTrade.API.Model;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public interface IClientCurrencyTradeCreator
{
    public void Create(TradeCreationModel tradeModel);
}

public class ClientCurrencyTradeCreator : IClientCurrencyTradeCreator
{
    private IBrokerCurrencyTradeCreator _brokerTradeCreator;
    private ICoefficientProvider _coefficientProvider;
    public ClientCurrencyTradeCreator(IBrokerCurrencyTradeCreator brokerTradeCreator, 
        ICoefficientProvider coefficientProvider)
    {
        _brokerTradeCreator = brokerTradeCreator;
        _coefficientProvider = coefficientProvider;
    }
    
    public void Create(TradeCreationModel tradeModel)
    {
        var brokerTrade = _brokerTradeCreator.Create(tradeModel);
        var c = _coefficientProvider.GetCoefficient();
        var buyAmountCalculated = brokerTrade.BuyAmount / c;
        var clientTrade = new ClientCurrencyTrade(brokerTrade.BuyCcy, brokerTrade.SellCcy,
            buyAmountCalculated, brokerTrade.SellAmount, brokerTrade);
    }
}