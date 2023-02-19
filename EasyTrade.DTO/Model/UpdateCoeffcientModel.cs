using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EasyTrade.DAL.Model;

namespace EasyTrade.DTO.Model;

public class UpdateCoefficientModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(TradeOperation))]
    public TradeOperation Operation { get; set; }
    public decimal Coefficient { get; set; }
    public DateTimeOffset DateTime { get; set; }
}

public class UpdateCurrencyTradeCoefficientModel : UpdateCoefficientModel
{
    [MaxLength(3)]
    [MinLength(3)]
    public string FirstIsoCode { get; set; }
    [MaxLength(3)]
    [MinLength(3)]
    public string SecondIsoCode { get; set; }
}