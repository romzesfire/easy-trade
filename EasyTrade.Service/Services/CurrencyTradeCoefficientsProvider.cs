using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class CurrencyTradeCoefficientsProvider : ICurrencyTradeCoefficientsProvider
{
    private readonly IRepository<CurrencyTradeCoefficient, (string?, string?)> _repo;
    public CurrencyTradeCoefficientsProvider(IRepository<CurrencyTradeCoefficient, (string?, string?)> repo)
    {
        _repo = repo;
    }

    public async Task<CurrencyTradeCoefficientResponse> GetCoefficient(string? firstIso, string? secondIso)
    {
        var c = await _repo.Get((firstIso, secondIso));
        return (CurrencyTradeCoefficientResponse)c;
    }
    
    public (IEnumerable<CurrencyTradeCoefficientResponse>, int) GetCoefficientsLimit(int limit, int offset)
    {
        var result = _repo.GetLimited(limit, offset);
        return (result.Item1
            .Select(c=>(CurrencyTradeCoefficientResponse)c), result.Item2);
    }
}