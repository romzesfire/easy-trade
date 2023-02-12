using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ITerminologyApi
{
    public IEnumerable<Currency> GetAvailableCurrencies();
}