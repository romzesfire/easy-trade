
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IBrokerCurrencyTradeCreator
{
    public BrokerCurrencyTrade Create(TradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
}