using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.DTO.Model;

[BindProperties(SupportsGet = true)]

public class PagingRequestModel
{
    [Range(1, 20)]
    public int Limit { get; set; } = 20;
    public int Offset { get; set; }
}