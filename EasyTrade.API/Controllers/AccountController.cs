using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private IOperationProvider _operationProvider;
    private readonly IBalanceProvider _balanceProvider;

    public AccountController(IOperationProvider operationProvider, IBalanceProvider balanceProvider)
    {
        _operationProvider = operationProvider;
        _balanceProvider = balanceProvider;
    }
    
    [HttpGet("Operations")]
    public IActionResult GetOperations(PagingRequestModel model)
    {
        var balances = _operationProvider.GetOperations(model.Limit, model.Offset);
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
        var balance = _balanceProvider.GetBalance(isoCode);
        return Ok(balance);
    }
    
    [HttpPost("Withdraw")]
    public IActionResult WithdrawToCard(string ccy, decimal amount,  
        string iban = null, string swift = null)
    {
        return Ok();
    }
}