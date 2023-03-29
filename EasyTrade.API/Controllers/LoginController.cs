using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EasyTrade.API.Configuration;
using EasyTrade.API.Validation;
using EasyTrade.Domain.Abstractions;
using EasyTrade.Service.Services.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EasyTrade.API.Controllers;

public class LoginController : Controller
{
    private ISecurityKeyValidator _securityKeyValidator;
    
    public LoginController(ISecurityKeyValidator securityKeyValidator)
    {
        _securityKeyValidator = securityKeyValidator;
    }
    
    [HttpGet("Login")]
    public IActionResult Login(string username, string securityKey)
    {
        var role = _securityKeyValidator.CheckRole(securityKey);
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, username),
            new (ClaimTypes.Role, role)
        };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
            
        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}