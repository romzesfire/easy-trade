
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IBrokerCurrencyTradeCreator
{
    public BrokerCurrencyTrade Create(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
    public BrokerCurrencyTrade Create(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
}