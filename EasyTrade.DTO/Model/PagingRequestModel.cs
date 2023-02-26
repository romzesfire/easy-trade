using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.DTO.Model;

[BindProperties(SupportsGet = true)]

public class PagingRequestModel
{
    [Range(1, 20)]
    [FromBody]
    [BindProperty(SupportsGet = true)]
    public int Limit { get; set; } = 20;
    [BindProperty(SupportsGet = true)]
    [FromBody]
    public int Offset { get; set; }
}