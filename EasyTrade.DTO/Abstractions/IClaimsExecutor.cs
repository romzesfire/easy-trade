using System.Security.Claims;

namespace EasyTrade.DTO.Abstractions;

public interface IClaimsExecutor
{
    Guid GetUserId(IEnumerable<Claim> claims);
    string GetRole(IEnumerable<Claim> claims);
}