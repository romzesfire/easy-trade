
using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    
    private readonly IOperationRecorder _updateBalance;
    private readonly IDataRecorder<UpdateCurrencyTradeCoefficientModel> _updateCcyTradeCoefficient;
    private readonly ICurrencyTradeCoefficientsProvider _coefficientsProvider;
    public AdministratorController(IOperationRecorder updateBalance,
        IDataRecorder<UpdateCurrencyTradeCoefficientModel> updateCcyTradeCoefficient,
        ICurrencyTradeCoefficientsProvider coefficientsProvider)
    {
        _updateBalance = updateBalance;
        _updateCcyTradeCoefficient = updateCcyTradeCoefficient;
        _coefficientsProvider = coefficientsProvider;
    }
    
    [HttpPost("ReplenishBalance")]
    public async Task<IActionResult> ReplenishBalance(UpdateAdminBalanceModel balanceModel)
    {
        await _updateBalance.Record(balanceModel, balanceModel.UserId);
        return Ok();
    }

    [HttpPost("UpdateCoefficient")]
    public async Task<IActionResult> UpdateCurrencyTradeCoefficient(UpdateCurrencyTradeCoefficientModel updateCoefficientModel)
    {
        await _updateCcyTradeCoefficient.Record(updateCoefficientModel);
        return Ok();
    }

    [HttpGet("GetCoefficient")]
    public IActionResult GetCoeficient([MaxLength(3)] [MinLength(3)] string? firstIso,
        [MaxLength(3)] [MinLength(3)] string? secondIso)
    {
        var c =_coefficientsProvider.GetCoefficient(firstIso, secondIso);
        return Ok(c);
    }
    [HttpGet("GetCoefficients")]
    public IActionResult GetCoeficients([FromQuery]PagingRequestModel model)
    {
        var c = _coefficientsProvider.GetCoefficientsLimit(model.Limit, model.Offset);
        return Ok(c.Item1);
    }
}