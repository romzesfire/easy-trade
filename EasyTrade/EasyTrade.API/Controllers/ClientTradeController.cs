using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private IEasyTradeDbContext _db;
    public ClientTradeController(ILogger<ClientTradeController> logger, IEasyTradeDbContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }

    [HttpPost("CreateTrade")]
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
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}