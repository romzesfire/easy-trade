using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model.Repository;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class CurrencyTradeCoefficientsProvider : ICurrencyTradeCoefficientsProvider
{
    private IRepository<CurrencyTradeCoefficient, (string?, string?)> _repo;
    public CurrencyTradeCoefficientsProvider(IRepository<CurrencyTradeCoefficient, (string?, string?)> repo)
    {
        _repo = repo;
    }

    public CurrencyTradeCoefficientResponse GetCoefficient(string? firstIso, string? secondIso)
    {
        var c = _repo.Get((firstIso, secondIso));
        return (CurrencyTradeCoefficientResponse)c;
    }
    
    public IEnumerable<CurrencyTradeCoefficientResponse> GetCoefficientsLimit(int limit, int offset)
    {
        var coefficients = _repo.GetLimited(limit, offset).Item1
            .Select(c=>(CurrencyTradeCoefficientResponse)c);
        return coefficients;
    }
}