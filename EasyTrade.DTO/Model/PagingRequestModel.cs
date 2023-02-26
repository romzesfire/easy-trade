using System.ComponentModel.DataAnnotations;

namespace EasyTrade.DTO.Model;

public class PagingRequestModel
{
    [Range(1, 20)]
    public int Limit { get; set; } = 20;
    public int Offset { get; set; }
}