using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;


public interface ICurrenciesProvider
{
    public (IEnumerable<CurrencyResponse>, int) GetCurrencies(int limit, int offset);
    public Task<CurrencyResponse> GetCurrency(string isoCode);
}