using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;

    public ClientTradeController(ILogger<ClientTradeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateTrade(string buyCcy, string sellCcy, 
        decimal? buyAmount = null,  decimal? sellAmount = null)
    {
        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetBalance(string ccy)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  string iban = null, string swift = null)
    {
        return Ok();
    }
}