using NUnit.Framework;

namespace EasyTrade.Test.Model;


public abstract class QuotesCalculatorModel
{
    public decimal Price { get; set; }
    public decimal Result { get; set; }
}

public class QuotesSellCalculatorModel : QuotesCalculatorModel
{
    public decimal BuyAmount { get; }
    public QuotesSellCalculatorModel(decimal price, decimal buyAmount, decimal result)
    {
        BuyAmount = buyAmount;
        Price = price;
        Result = result;
    }
}

public class QuotesBuyCalculatorModel : QuotesCalculatorModel
{
    public decimal SellAmount { get; }

    public QuotesBuyCalculatorModel(decimal price, decimal sellAmount, decimal result)
    {
        SellAmount = sellAmount;
        Price = price;
        Result = result;
    }
}