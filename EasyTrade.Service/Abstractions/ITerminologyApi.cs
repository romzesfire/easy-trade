using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Abstractions;

public interface ITerminologyApi
{
    public IEnumerable<Currency> GetAvailableCurrencies();
}