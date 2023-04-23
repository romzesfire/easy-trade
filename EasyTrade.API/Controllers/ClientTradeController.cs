
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClientTradeController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private readonly IClientCurrencyTradeCreator _tradeCreator;
    private readonly ICurrencyTradesProvider _currencyTradesProvider;
    private readonly IClaimsExecutor _claimsExecutor;
    public ClientTradeController(ILogger<ClientTradeController> logger,
        IClientCurrencyTradeCreator tradeCreator, ICurrencyTradesProvider currencyTradesProvider, 
        IClaimsExecutor claimsExecutor)
    {
        _logger = logger;
        _tradeCreator = tradeCreator;
        _currencyTradesProvider = currencyTradesProvider;
        _claimsExecutor = claimsExecutor;
    }

    [HttpPost("Buy")]
    public async Task<IActionResult> Buy(BuyTradeCreationModel buyModel)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        await _tradeCreator.Create(buyModel, user);
        return Ok();
    }
    
    [HttpPost("Sell")]
    public async Task<IActionResult> Sell(SellTradeCreationModel sellModel)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        await _tradeCreator.Create(sellModel, user);
        return Ok();
    }
    
    [HttpGet("Trades")]
    public IActionResult GetTrades([FromQuery]PagingRequestModel model)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        var trades = _currencyTradesProvider.GetTrades(model.Limit, model.Offset, user);
        return Ok(trades.Item1);
    }
    
    [HttpGet("Trades/{id}")]
    public async Task<IActionResult> GetTrade(int id)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        var trade = await _currencyTradesProvider.GetTrade(id, user);
        return Ok(trade);
    }
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}