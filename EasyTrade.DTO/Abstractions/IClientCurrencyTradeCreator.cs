using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;


public interface IClientCurrencyTradeCreator
{
    public void Create(TradeCreationModel tradeModel);
}
