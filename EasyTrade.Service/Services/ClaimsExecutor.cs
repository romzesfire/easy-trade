using System.Security.Claims;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Exceptions;

namespace EasyTrade.Service.Services;

public class ClaimsExecutor : IClaimsExecutor
{
    public Guid GetUserId(IEnumerable<Claim> claims)
    {
        var userId = claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        if (userId == null)
            throw new InvalidUserException();

        var result = Guid.TryParse(userId.Value, out var id);
        if(!result)
            throw new InvalidUserException();
        return id;
    }
    
    public string GetRole(IEnumerable<Claim> claims)
    {
        var role = claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role");
        
        if (role == null)
            throw new InvalidUserException();
        
        return role.Value;
    }
}