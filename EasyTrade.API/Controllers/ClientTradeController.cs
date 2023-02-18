
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private IClientCurrencyTradeCreator _tradeCreator;
    public ICurrencyTradesProvider CurrencyTradesProvider;
    public ClientTradeController(ILogger<ClientTradeController> logger,
        IClientCurrencyTradeCreator tradeCreator, ICurrencyTradesProvider currencyTradesProvider)
    {
        _logger = logger;
        _tradeCreator = tradeCreator;
        CurrencyTradesProvider = currencyTradesProvider;
    }
    //ДТО зависит от DAL это ок?
    //2 Get метода
    //DTO
    //REST API - посмотреть структуру и применить к методам
    //БД в сервисы убрать
    [HttpPost("Buy")]//Buy, Sell
    public IActionResult Buy(BuyTradeCreationModel buyModel)
    {
        //FluentValidator
        //CQRS
        //ModelState.Values.Where(s=>s.)
        _tradeCreator.Create(buyModel);
        return Ok();
    }
    
    [HttpPost("Sell")]//Buy, Sell
    public IActionResult Sell(SellTradeCreationModel sellModel)
    {
        _tradeCreator.Create(sellModel);
        return Ok();
    }
    
    [HttpGet("Trades")]
    public IActionResult GetTrades(int limit = 20, int offset = 0)
    {
        var trades = CurrencyTradesProvider.GetTrades(limit, offset);
        return Ok(trades);
    }
    
    [HttpGet("Trades/{id}")]
    public IActionResult GetTrade(uint id)
    {
        var trade = CurrencyTradesProvider.GetTrade(id);
        return Ok(trade);
    }
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}