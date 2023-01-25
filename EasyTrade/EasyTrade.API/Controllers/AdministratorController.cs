using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

public class AdministratorController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;

    public AdministratorController(ILogger<ClientTradeController> logger)
    {
        _logger = logger;
    }
    
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