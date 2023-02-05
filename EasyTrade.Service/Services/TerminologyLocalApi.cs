using EasyTrade.DTO.Model;
using EasyTrade.Service.Abstractions;

namespace EasyTrade.Service.Services;

public class TerminologyLocalApi : ITerminologyApi
{
  private Currency[] _availableCurrencies = 
  {
    new Currency(1,"Rouble", "RUB" ),
    new Currency(2, "Euro", "EUR"),
    new Currency(3, "Dollar USA", "USD"),
  };
  
  public IEnumerable<Currency> GetAvailableCurrencies() => _availableCurrencies;
  
}