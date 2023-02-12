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
}