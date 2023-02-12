using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ITerminologyApi
{
    public IEnumerable<Currency> GetAvailableCurrencies();
}