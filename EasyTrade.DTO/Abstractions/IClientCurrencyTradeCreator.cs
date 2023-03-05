using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;


public interface IClientCurrencyTradeCreator
{
    public Task Create(BuyTradeCreationModel tradeModel);
    public Task Create(SellTradeCreationModel tradeModel);
}
