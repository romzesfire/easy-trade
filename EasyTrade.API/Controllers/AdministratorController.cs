
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    private readonly ILogger<ClientTradeController> _logger;
    private IBalanceProvider _balanceProvider;
    private ICoefficientProvider _coefficientProvider;
    public AdministratorController(ILogger<ClientTradeController> logger, IBalanceProvider balanceProvider, 
        ICoefficientProvider coefficientProvider)
    {
        _logger = logger;
        _balanceProvider = balanceProvider;
        _coefficientProvider = coefficientProvider;
    }
    
    [HttpPost("ReplenishBalance")]
    public IActionResult ReplenishBalance(UpdateBalanceModel balanceModel)
    {
        _balanceProvider.GetBalance(balanceModel.IsoCode).Amount += balanceModel.Amount;
        return Ok();
    }

    [HttpPost("UpdateCoefficient")]
    public IActionResult UpdateCoefficient(UpdateCoefficientModel updateCoefficientModel)
    {

        return Ok();
    }
}