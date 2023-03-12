using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IBrokerCurrencyTradeCreator
{

    public Task<BrokerCurrencyTrade> Create(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
    public Task<BrokerCurrencyTrade> Create(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
}