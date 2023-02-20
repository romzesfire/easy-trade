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

    public Quote Get(string from, string to)
    {
        var quote = _api.GetQuote(from, to, 1).Result;
        return new Quote(quote);
    }
}