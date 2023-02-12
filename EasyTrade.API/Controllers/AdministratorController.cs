using EasyTrade.DAL.Model;
using EasyTrade.DTO.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private IBalanceProvider _balanceProvider;
    private IDataSaver _dataSaver;
    public AdministratorController(ILogger<ClientTradeController> logger, IBalanceProvider balanceProvider, 
        IDataSaver dataSaver)
    {
        _logger = logger;
        _balanceProvider = balanceProvider;
        _dataSaver = dataSaver;
    }
    
    [HttpPost("ReplenishBalance")]
    public IActionResult ReplenishBalance(ReplenishBalanceModel balanceModel)
    {
        _balanceProvider.GetBalance(balanceModel.IsoCode).Amount += balanceModel.Amount;
        _dataSaver.Save();
        return Ok();
    }

    [HttpPost("RefreshCoefficient")]
    public IActionResult RefreshCoefficient(decimal coefficient)
    {
        return Ok();
    }
}