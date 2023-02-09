using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;


public interface ICurrenciesProvider
{
    public IEnumerable<Currency> GetCurrencies();
}