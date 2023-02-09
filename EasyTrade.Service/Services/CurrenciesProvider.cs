using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Abstractions;

namespace EasyTrade.Service.Services;


public class CurrenciesProvider : ICurrenciesProvider
{
    private ITerminologyApi _terminology;

    public CurrenciesProvider(ITerminologyApi terminology)
    {
        _terminology = terminology;
    }

    public IEnumerable<Currency> GetCurrencies()
    {
        return _terminology.GetAvailableCurrencies();
    }
}