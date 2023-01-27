using EasyTrade.DAL.Model;
using EasyTrade.Service.Abstractions;

namespace EasyTrade.Service.Services;

public interface ICurrenciesProvider
{
    public IEnumerable<Currency> GetCurrencies();
}

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