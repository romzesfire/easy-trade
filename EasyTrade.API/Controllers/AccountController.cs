using System.ComponentModel.DataAnnotations;
using EasyTrade.DTO.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EasyTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private IBalanceProvider _balanceProvider;
    public AccountController(IBalanceProvider balanceProvider)
    {
        _balanceProvider = balanceProvider;
    }
    
    [HttpGet("Operations")]
    public IActionResult GetOperations(int limit = 20, int offset = 0)
    {
        var balances = _balanceProvider.GetOperations(limit, offset);
        return Ok(balances);
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