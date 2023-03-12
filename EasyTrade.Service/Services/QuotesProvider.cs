using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Services;


public class QuotesProvider : IQuotesProvider
{
    private readonly IQuotesApi _api;

    public QuotesProvider(IQuotesApi api)
    {
        _api = api;
    }

    public async Task<Quote> Get(string from, string to)
    {
        var quote = await _api.GetQuote(from, to, 1);
        return new Quote(quote);
    }
}