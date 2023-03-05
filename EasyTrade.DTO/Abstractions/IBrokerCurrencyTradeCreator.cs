using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IBrokerCurrencyTradeCreator
{
    public BrokerCurrencyTrade Create(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
    public BrokerCurrencyTrade Create(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
    public Task<BrokerCurrencyTrade> CreateAsync(BuyTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
    public Task<BrokerCurrencyTrade> CreateAsync(SellTradeCreationModel tradeModel, Currency buyCcy, Currency sellCcy);
}