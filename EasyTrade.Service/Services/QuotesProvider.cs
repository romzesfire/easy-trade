using EasyTrade.Service.Abstractions;
using EasyTrade.Service.Model;

namespace EasyTrade.Service.Services;

public interface IQuotesProvider
{
    public Quote Get(string from, string to);
}

public class QuotesProvider : IQuotesProvider
{
    private IQuotesApi _api;

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