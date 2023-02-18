using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EasyTrade.DAL.Model;

public enum TradeOperation
{
    CurrencyTrade
}

public class TradeCoefficient
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public decimal Coefficient { get; set; }
}

public class CurrencyTradeCoefficient : TradeCoefficient
{
    public Currency? FirstCcy { get; set; }
    public Currency? SecondCcy { get; set; }
}
