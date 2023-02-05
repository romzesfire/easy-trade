using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ITradeSaver
{
    public void Save(ClientCurrencyTrade trade);
}