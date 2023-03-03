using EasyTrade.Domain.Model;

namespace EasyTrade.Service.Model.ResponseModels;

public class CurrencyResponse
{
    public string Name { get; set; }
    public string IsoCode { get; set; }

    public static explicit operator CurrencyResponse(Currency ccy)
    {
        if (ccy == null)
        {
            return null;
        }
        return new CurrencyResponse()
        {
            IsoCode = ccy.IsoCode,
            Name = ccy.Name
        };
    }
}