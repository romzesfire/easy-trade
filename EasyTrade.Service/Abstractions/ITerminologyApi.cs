using EasyTrade.DAL.Model;

namespace EasyTrade.Service.Abstractions;

public interface ITerminologyApi
{
    public IEnumerable<Currency> GetAvailableCurrencies();
}