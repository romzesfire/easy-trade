using EasyTrade.DAL.Model;
using EasyTrade.Service.Abstractions;

namespace EasyTrade.Service.Services;

public class TerminologyLocalApi : ITerminologyApi
{
  private Currency[] _availableCurrencies = 
  {
    new Currency() { Name = "Rouble", Id = 1, IsoCode = "RUB" },
    new Currency() { Name = "Euro", Id = 3, IsoCode = "EUR" },
    new Currency() { Name = "Dollar USA", Id = 2, IsoCode = "USD" },
  };
  
  public IEnumerable<Currency> GetAvailableCurrencies() => _availableCurrencies;
  
}