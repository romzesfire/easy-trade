
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
    private IDataSaver _dataSaver;
    private ICoefficientProvider _coefficientProvider;
    public AdministratorController(ILogger<ClientTradeController> logger, IBalanceProvider balanceProvider, 
        IDataSaver dataSaver, ICoefficientProvider coefficientProvider)
    {
        _logger = logger;
        _balanceProvider = balanceProvider;
        _dataSaver = dataSaver;
        _coefficientProvider = coefficientProvider;
    }
    
    [HttpPost("ReplenishBalance")]
    public IActionResult ReplenishBalance(ReplenishBalanceModel balanceModel)
    {
        _balanceProvider.GetBalance(balanceModel.IsoCode).Amount += balanceModel.Amount;
        _dataSaver.Save();
        return Ok();
    }

    [HttpPost("UpdateCoefficient")]
    public IActionResult UpdateCoefficient(UpdateCoefficientModel updateCoefficientModel)
    {
        var coefficient = _coefficientProvider.GetCoefficient(updateCoefficientModel.Operation);
        coefficient.Coefficient = updateCoefficientModel.Coefficient;
        _dataSaver.Save();
        return Ok();
    }
}