using EasyTrade.Service.Model;

namespace EasyTrade.DTO.Model;

public class Quote
{
    public string FromCcy { get; }
    public string ToCcy { get; }
    public decimal Price { get; }

    public Quote(QuoteResponse quote)
    {
        FromCcy = quote.Query.From;
        ToCcy = quote.Query.To;
        Price = quote.Result;
    }
}