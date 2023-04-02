using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IOperationProvider _operationProvider;
    private readonly IBalanceProvider _balanceProvider;
    private IClaimsExecutor _claimsExecutor;
    public AccountController(IOperationProvider operationProvider, IBalanceProvider balanceProvider, 
        IClaimsExecutor claimsExecutor)
    {
        _operationProvider = operationProvider;
        _balanceProvider = balanceProvider;
        _claimsExecutor = claimsExecutor;
    }
    
    [HttpGet("Operations")]
    public IActionResult GetOperations([FromQuery]PagingRequestModel model)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        var balances = _operationProvider.GetOperations(model.Limit, model.Offset, user);
        var x = User.Claims;
        return Ok(balances.Item1);
    }
    
    // [HttpGet("Balances/{id}")]
    // public IActionResult GetBalanceById(uint id)
    // {
    //     var balance = _balanceProvider.GetBalance(id);
    //     return Ok(balance);
    // }
     
    [HttpGet("Balances/{isoCode}")]
    public IActionResult GetBalanceByCode([MaxLength(3)]string isoCode)
    {
        var user = _claimsExecutor.GetUserId(User.Claims);
        var balance = _balanceProvider.GetBalance(isoCode, user);
        return Ok(balance);
    }
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}