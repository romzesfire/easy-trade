
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    //Как добавлять БД в DI если она в DAL?
    //2 Get метода
    //DTO
    //Интерфейсы сервисов в DTO
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
        //FluentValidator
        //CQRS
        //ModelState.Values.Where(s=>s.)
        _tradeCreator.Create(sellModel);
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