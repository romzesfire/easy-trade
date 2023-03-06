using System.Collections;
using EasyTrade.Domain.Exception;
using EasyTrade.Domain.Model;
using EasyTrade.Domain.Services;
using EasyTrade.Test.Model;

namespace EasyTrade.Test;

[TestFixture]
public class BalanceCalculatorTests
{
    public static IEnumerable ValidTestCases
    {
        get
        {
            yield return new TestCaseData(new BalanceCalculatorModelValid()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                },
                Operations = new[]
                {
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 1
                    },
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = 200,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 2
                    }
                },
                BalanceResult = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1100,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                }
            });
            
            yield return new TestCaseData(new BalanceCalculatorModelValid()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = null,
                Operations = new[]
                {
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 1
                    },
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = 200,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 2
                    }
                },
                BalanceResult = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 100,
                    CurrencyIso = "USD",
                    Id = -1,
                    Version = Guid.NewGuid()
                }
            });
            
            yield return new TestCaseData(new BalanceCalculatorModelValid()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                },
                Operations = new[]
                {
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 1
                    },

                },
                BalanceResult = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 900,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                }
            });
            
            yield return new TestCaseData(new BalanceCalculatorModelValid()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                },
                Operations = Array.Empty<Operation>(),
                BalanceResult = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                }
            });
        }
    }

    public static IEnumerable InvalidTestCases
    {
        get
        {
            yield return new TestCaseData(new BalanceCalculatorModel()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                },
                Operations = new[]
                {
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 1
                    },
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -1100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 2
                    }
                }
            });
            
            yield return new TestCaseData(new BalanceCalculatorModel()
            {
                Currency = new Currency()
                {
                    IsoCode = "USD",
                    Id = 1,
                    Name = "Dollar USA"
                },
                BalanceInput = new Balance()
                {
                    Currency = new Currency()
                    {
                        IsoCode = "USD",
                        Id = 1,
                        Name = "Dollar USA"
                    },
                    Amount = 1000,
                    CurrencyIso = "USD",
                    Id = 1,
                    Version = Guid.NewGuid()
                },
                Operations = new[]
                {
                    new Operation()
                    {
                        Currency = new Currency()
                        {
                            IsoCode = "USD",
                            Id = 1,
                            Name = "Dollar USA"
                        },
                        Amount = -1100,
                        CurrencyIso = "USD",
                        DateTime = DateTimeOffset.Now,
                        Id = 2
                    }
                }
            });
        }
    }

    [Test]
    [TestCaseSource(nameof(ValidTestCases))]
    public void CalculateValidTests(BalanceCalculatorModelValid calculatorModelValid)
    {
        var calculator = new BalanceCalculator();
        var result = calculator.Calculate(calculatorModelValid.BalanceInput,
            calculatorModelValid.Operations, calculatorModelValid.Currency);
        
        Assert.That(calculatorModelValid.BalanceResult.Amount == result.Amount 
                    && calculatorModelValid.BalanceResult.Id == result.Id, "Invalid balance calculation");
    }
    
    
    [Test]
    [TestCaseSource(nameof(InvalidTestCases))]
    public void CalculateInvalidTests(BalanceCalculatorModel calculatorModelValid)
    {
        var calculator = new BalanceCalculator();

        Assert.Throws<NotEnoughAssetsException>( ()=>
        {
            calculator.Calculate(calculatorModelValid.BalanceInput,
                calculatorModelValid.Operations, calculatorModelValid.Currency);
        }, "Invalid operation is not detected");
    }
}