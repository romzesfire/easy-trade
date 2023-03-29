namespace EasyTrade.Domain.Abstractions;

public interface ISecurityKeyValidator
{
    public string CheckRole(string securityKey);
}