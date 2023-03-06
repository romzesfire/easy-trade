using System.Collections;
using EasyTrade.Domain.Services;
using EasyTrade.Test.Model;
using NUnit.Framework;

namespace EasyTrade.Test;

[TestFixture]
public class PriceMarkupCalculatorTests
{
    public static IEnumerable BuyCases
    {
        get
        {
            yield return new TestCaseData(new PriceMarkupBuyCalculatorModel(10m, 2m, 5m));
            yield return new TestCaseData(new PriceMarkupBuyCalculatorModel(100, 2.5m, 40m));
        }
    }
    public static IEnumerable SellCases
    {
        get
        {
            yield return new TestCaseData(new PriceMarkupSellCalculatorModel(25m, 4m, 100m));
            yield return new TestCaseData(new PriceMarkupSellCalculatorModel(20m, 1.2m, 24m));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(SellCases))]
    public void CalculateSellAmountTest(PriceMarkupSellCalculatorModel calculatorModel)
    {
        var calculator = new PriceMarkupCalculator();
        var result = calculator.CalculateSellAmount(calculatorModel.SellAmount, calculatorModel.Coefficient);
        Assert.That(result.Equals(calculatorModel.Result), 
            $"Calculation result is {result}, but expected {calculatorModel.Result}");
    }
    
    [Test]
    [TestCaseSource(nameof(BuyCases))]
    public void CalculateBuyAmountTest(PriceMarkupBuyCalculatorModel calculatorModel)
    {
        var calculator = new PriceMarkupCalculator();
        var result = calculator.CalculateBuyAmount(calculatorModel.BuyAmount, calculatorModel.Coefficient);
        Assert.That(result.Equals(calculatorModel.Result), 
            $"Calculation result is {result}, but expected {calculatorModel.Result}");
    }
}