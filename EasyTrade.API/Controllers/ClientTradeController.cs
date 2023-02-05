using EasyTrade.API.Model;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
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
    private ICurrenciesValidator _currenciesValidator;
    public ClientTradeController(ILogger<ClientTradeController> logger, EasyTradeDbContext dbContext, 
        IClientCurrencyTradeCreator tradeCreator, ICurrenciesValidator currenciesValidator)
    {
        _logger = logger;
        _db = dbContext;
        _tradeCreator = tradeCreator;
        _currenciesValidator = currenciesValidator;
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
        ModelErrorCollection
        var result = _tradeCreator.Create(buyModel);
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