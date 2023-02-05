namespace EasyTrade.DTO.Abstractions;

public interface ICurrenciesValidator
{
    public bool IsValidCcy(string iso);
}