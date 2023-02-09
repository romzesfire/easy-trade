using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface ITradesProvider
{
    public IEnumerable<Trade> GetTrades(int limit, int offset);
    public Trade GetTrade(uint id);
}