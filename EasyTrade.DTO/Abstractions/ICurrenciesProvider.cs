using EasyTrade.DAL.Model;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;


public interface ICurrenciesProvider
{
    public IEnumerable<CurrencyResponse> GetCurrencies(int limit, int offset);
    public CurrencyResponse GetCurrency(string isoCode);
}