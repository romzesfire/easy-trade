using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;


public class CurrenciesProvider : ICurrenciesProvider
{
    private IRepository<Currency, string> _ccyProvider;
    public CurrenciesProvider(IRepository<Currency, string> ccyProvider)
    {
        _ccyProvider = ccyProvider;
    }

    public IEnumerable<CurrencyResponse> GetCurrencies(int limit, int offset)
    {
        return _ccyProvider.GetLimited(limit, offset).Select(c=>(CurrencyResponse)c);
    }

    public CurrencyResponse GetCurrency(string isoCode)
    {
        return (CurrencyResponse)_ccyProvider.Get(isoCode);
    }
}