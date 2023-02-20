using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface ICurrencyTradeCoefficientsProvider
{
    CurrencyTradeCoefficientResponse GetCoefficient(string? firstIso, string? secondIso);
    IEnumerable<CurrencyTradeCoefficientResponse> GetCoefficientsLimit(int limit, int offset);
}