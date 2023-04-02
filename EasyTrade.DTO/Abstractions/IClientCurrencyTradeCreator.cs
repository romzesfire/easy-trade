using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;


public interface IClientCurrencyTradeCreator
{
    public Task Create(BuyTradeCreationModel tradeModel, Guid userId);
    public Task Create(SellTradeCreationModel tradeModel, Guid userId);
}
