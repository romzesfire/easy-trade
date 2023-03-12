using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IQuotesProvider
{
    public Task<Quote> Get(string from, string to);
}