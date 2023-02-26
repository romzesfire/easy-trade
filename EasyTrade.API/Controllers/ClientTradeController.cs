
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private IClientCurrencyTradeCreator _tradeCreator;
    public ICurrencyTradesProvider _currencyTradesProvider;
    public ClientTradeController(ILogger<ClientTradeController> logger,
        IClientCurrencyTradeCreator tradeCreator, ICurrencyTradesProvider currencyTradesProvider)
    {
        _logger = logger;
        _tradeCreator = tradeCreator;
        _currencyTradesProvider = currencyTradesProvider;
    }

    [HttpPost("Buy")]//Buy, Sell
    public IActionResult Buy(BuyTradeCreationModel buyModel)
    {
        //FluentValidator
        //CQRS
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
    public IActionResult GetTrades([FromBody]PagingRequestModel model)
    {
        var trades = _currencyTradesProvider.GetTrades(model.Limit, model.Offset);
        return Ok(trades);
    }
    
    [HttpGet("Trades/{id}")]
    public IActionResult GetTrade(int id)
    {
        var trade = _currencyTradesProvider.GetTrade(id);
        return Ok(trade);
    }
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}