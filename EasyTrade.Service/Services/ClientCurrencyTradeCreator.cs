using EasyTrade.DAL.Model;

namespace EasyTrade.Service.Services;

public interface IClientCurrencyTradeCreator
{
    public ClientCurrencyTrade Create(string buyCcy, string sellCcy, 
        decimal? buyAmount = null,  decimal? sellAmount = null);
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
    
    public ClientCurrencyTrade Create(string buyCcy, string sellCcy, 
        decimal? buyAmount = null,  decimal? sellAmount = null)
    {
        var brokerTrade = _brokerTradeCreator.Create(buyCcy, sellCcy, buyAmount, sellAmount);
        var c = _coefficientProvider.GetCoefficient();
        var buyAmountCalculated = brokerTrade.BuyAmount / c;
        var clientTrade = new ClientCurrencyTrade(brokerTrade.BuyCcy, brokerTrade.SellCcy,
            buyAmountCalculated, brokerTrade.SellAmount, brokerTrade);

        return clientTrade;
    }
}