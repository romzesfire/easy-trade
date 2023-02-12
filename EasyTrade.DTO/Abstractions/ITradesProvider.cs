using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ITradesProvider
{
    public IEnumerable<CurrencyTrade> GetTrades(int limit, int offset);
    public CurrencyTrade GetTrade(uint id);
}