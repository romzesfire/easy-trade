namespace EasyTrade.Test.Model;

public abstract class PriceMarkupCalculatorModel
{
    public decimal Coefficient { get; set; }
    public decimal Result { get; set; }
}

public class PriceMarkupSellCalculatorModel : PriceMarkupCalculatorModel
{
    public decimal SellAmount { get; }
    public PriceMarkupSellCalculatorModel(decimal sellAmount, decimal coefficient, decimal result)
    {
        SellAmount = sellAmount;
        Result = result;
        Coefficient = coefficient;
    }
}

public class PriceMarkupBuyCalculatorModel : PriceMarkupCalculatorModel
{
    public decimal BuyAmount { get; }

    public PriceMarkupBuyCalculatorModel(decimal buyAmount, decimal coefficient, decimal result)
    {
        BuyAmount = buyAmount;
        Result = result;
        Coefficient = coefficient;
    }
}