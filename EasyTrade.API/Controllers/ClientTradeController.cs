using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private EasyTradeDbContext _db;
    private IClientCurrencyTradeCreator _tradeCreator;
    
    public ClientTradeController(ILogger<ClientTradeController> logger, EasyTradeDbContext dbContext, 
        IClientCurrencyTradeCreator tradeCreator)
    {
        _logger = logger;
        _db = dbContext;
        _tradeCreator = tradeCreator;
    }

    [HttpPost("CreateTrade")]
    public IActionResult CreateTrade(string buyCcy, string sellCcy, 
        decimal? buyAmount = null,  decimal? sellAmount = null)
    {
        var result = _tradeCreator.Create(buyCcy, sellCcy, buyAmount, sellAmount);
        result = _db.AddTrade(result);
        return Ok(result);
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