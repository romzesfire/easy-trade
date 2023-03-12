using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface ICurrencyTradeCoefficientsProvider
{
    public Task<CurrencyTradeCoefficientResponse> GetCoefficient(string? firstIso, string? secondIso);
    public  (IEnumerable<CurrencyTradeCoefficientResponse>, int) GetCoefficientsLimit(int limit, int offset);
}