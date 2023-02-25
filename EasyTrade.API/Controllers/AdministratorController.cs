
using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    
    private IDataRecorder<UpdateBalanceModel> _updateBalance;
    private IDataRecorder<UpdateCurrencyTradeCoefficientModel> _updateCcyTradeCoefficient;
    private ICurrencyTradeCoefficientsProvider _coefficientsProvider;
    public AdministratorController(IDataRecorder<UpdateBalanceModel> updateBalance,
        IDataRecorder<UpdateCurrencyTradeCoefficientModel> updateCcyTradeCoefficient,
        ICurrencyTradeCoefficientsProvider coefficientsProvider)
    {
        _updateBalance = updateBalance;
        _updateCcyTradeCoefficient = updateCcyTradeCoefficient;
        _coefficientsProvider = coefficientsProvider;
    }
    
    [HttpPost("ReplenishBalance")]
    public IActionResult ReplenishBalance(UpdateBalanceModel balanceModel)
    {
        _updateBalance.Record(balanceModel);
        return Ok();
    }

    [HttpPut("UpdateCoefficient")] // на коэффециенты хорошо ляжет REST API (CRUD), не хватает отдельного Create
    public IActionResult UpdateCurrencyTradeCoefficient(UpdateCurrencyTradeCoefficientModel updateCoefficientModel)
    {
        _updateCcyTradeCoefficient.Record(updateCoefficientModel);
        return Ok();
    }

    [HttpGet("GetCoefficient")]
    public IActionResult GetCoeficient([MaxLength(3)] [MinLength(3)] string? firstIso, // лучше модельку
        [MaxLength(3)] [MinLength(3)] string? secondIso)
    {
        var c =_coefficientsProvider.GetCoefficient(firstIso, secondIso);
        return Ok(c);
    }
    [HttpGet("GetCoefficients")]
    public IActionResult GetCoeficients(int limit, int offset) // часто встречаются эти параметры для пейджинга - может сделать общую модельку?
    {
        var c = _coefficientsProvider.GetCoefficientsLimit(limit, offset);
        return Ok(c);
    }
}