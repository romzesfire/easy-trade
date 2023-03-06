using System.Collections;
using EasyTrade.Domain.Services;
using EasyTrade.Test.Model;

namespace EasyTrade.Test;

public class QuotesCalculatorTests
{
    public static IEnumerable SellCases
    {
        get
        {
            yield return new TestCaseData(new QuotesSellCalculatorModel(5m, 100m, 20m));
            yield return new TestCaseData(new QuotesSellCalculatorModel(200m, 100m, 0.5m));
        }
    }
    public static IEnumerable BuyCases
    {
        get
        {
            yield return new TestCaseData(new QuotesBuyCalculatorModel(10m, 100m, 1000m));
            yield return new TestCaseData(new QuotesBuyCalculatorModel(1.5m, 100m, 150m));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(SellCases))]
    public void CalculateSellAmountTest(QuotesSellCalculatorModel calculatorModel)
    {
        var calculator = new QuotesCalculator();
        var result = calculator.CalculateSellAmount(calculatorModel.BuyAmount, calculatorModel.Price);
        Assert.That(result.Equals(calculatorModel.Result), 
            $"Calculation result is {result}, but expected {calculatorModel.Result}");
    }
    
    [Test]
    [TestCaseSource(nameof(BuyCases))]
    public void CalculateBuyAmountTest(QuotesBuyCalculatorModel calculatorModel)
    {
        var calculator = new QuotesCalculator();
        var result = calculator.CalculateBuyAmount(calculatorModel.SellAmount, calculatorModel.Price);
        Assert.That(result.Equals(calculatorModel.Result), 
            $"Calculation result is {result}, but expected {calculatorModel.Result}");
    }

    
}