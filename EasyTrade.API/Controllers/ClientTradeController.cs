
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private readonly IClientCurrencyTradeCreator _tradeCreator;
    private readonly ICurrencyTradesProvider _currencyTradesProvider;
    public ClientTradeController(ILogger<ClientTradeController> logger,
        IClientCurrencyTradeCreator tradeCreator, ICurrencyTradesProvider currencyTradesProvider)
    {
        _logger = logger;
        _tradeCreator = tradeCreator;
        _currencyTradesProvider = currencyTradesProvider;
    }

    [HttpPost("Buy")]
    public async Task<IActionResult> Buy(BuyTradeCreationModel buyModel)
    {
        //FluentValidator
        //CQRS
        await _tradeCreator.Create(buyModel);
        return Ok();
    }
    
    [HttpPost("Sell")]
    public async Task<IActionResult> Sell(SellTradeCreationModel sellModel)
    {
        await _tradeCreator.Create(sellModel);
        return Ok();
    }
    
    [HttpGet("Trades")]
    public IActionResult GetTrades([FromQuery]PagingRequestModel model)
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