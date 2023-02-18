using EasyTrade.DAL.Model;

namespace EasyTrade.Service.Model.ResponseModels;

public class TradeCoefficientResponse
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public decimal Coefficient { get; set; }
}

public class CurrencyTradeCoefficientResponse : TradeCoefficientResponse
{
    public CurrencyResponse? FirstCcy { get; set; }
    public CurrencyResponse? SecondCcy { get; set; }
    
    public static explicit operator CurrencyTradeCoefficientResponse(CurrencyTradeCoefficient coefficient)
    {
        return new CurrencyTradeCoefficientResponse()
        {
            Id = coefficient.Id,
            Coefficient = coefficient.Coefficient,
            DateTime = coefficient.DateTime,
            FirstCcy = (CurrencyResponse)coefficient.FirstCcy,
            SecondCcy = (CurrencyResponse)coefficient.SecondCcy
        };
    }
}
