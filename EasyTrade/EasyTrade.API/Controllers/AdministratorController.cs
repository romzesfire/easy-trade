using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

public class AdministratorController : ControllerBase
{
    [HttpPost]
    public IActionResult ReplenishBalance(string ccy, decimal amount)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult RefreshCoefficient(decimal coefficient, string ccy = null)
    {
        return Ok();
    }
}