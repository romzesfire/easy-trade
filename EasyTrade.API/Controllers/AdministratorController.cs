using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;

    public AdministratorController(ILogger<ClientTradeController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost("ReplenishBalance")]
    public IActionResult ReplenishBalance(string ccy, decimal amount)
    {
        return Ok();
    }

    [HttpPost("RefreshCoefficient")]
    public IActionResult RefreshCoefficient(decimal coefficient)
    {
        return Ok();
    }
}