using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Abstractions;


public interface ICurrenciesProvider
{
    public IEnumerable<Currency> GetCurrencies();
}