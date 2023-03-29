using EasyTrade.Domain.Abstractions;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services.Security;

public class SecurityKeyValidator : ISecurityKeyValidator
{
    public SecurityKeyValidator(){}

    public string CheckRole(string securityKey)
    {
        if (string.IsNullOrEmpty(securityKey))
            throw new InvalidSecurityKeyException();
        
        return "Admin";
    }
}