using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;


public class CurrenciesProvider : ICurrenciesProvider
{
    private readonly IRepository<Currency, string> _ccyProvider;
    public CurrenciesProvider(IRepository<Currency, string> ccyProvider)
    {
        _ccyProvider = ccyProvider;
    }

    public (IEnumerable<CurrencyResponse>, int) GetCurrencies(int limit, int offset)
    {
        var result = _ccyProvider.GetLimited(limit, offset);
        return (result.Item1.Select(c=>(CurrencyResponse)c), result.Item2);
    }

    public async Task<CurrencyResponse> GetCurrency(string isoCode)
    {
        return (CurrencyResponse) await _ccyProvider.Get(isoCode);
    }
}