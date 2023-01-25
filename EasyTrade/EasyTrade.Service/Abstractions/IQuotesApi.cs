using EasyTrade.Service.Model;
using Refit;

namespace EasyTrade.Service.Abstractions;

public interface IQuotesApi
{
    [Get("/convert")]
    public Task<QuoteResponse> GetQuote(string from, string to, int amount);
}