using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;

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