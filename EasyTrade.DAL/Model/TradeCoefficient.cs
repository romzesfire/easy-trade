using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EasyTrade.DAL.Model;

public enum TradeOperation
{
    [EnumMember(Value = "CurrencyTrade")]
    CurrencyTrade
}

public class TradeCoefficient
{
    public uint Id { get; set; }
    public TradeOperation Operation { get; set; }
    public decimal Coefficient { get; set; }
}