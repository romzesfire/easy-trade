using EasyTrade.Domain.Model;
using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.Service.Services;

public class OperationDbProvider : IOperationProvider
{
    private readonly IRepository<Operation, int> _balanceRepository;
    public OperationDbProvider(IRepository<Operation, int> balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }
    public async Task<OperationResponse> GetOperation(int id, Guid userId)
    {
        return (OperationResponse) await _balanceRepository.Get(id, userId);
    }

    public (IEnumerable<OperationResponse>, int) GetOperations(int limit, int offset, Guid userId)
    {
        var (operations, count) = _balanceRepository.GetLimited(limit, offset, userId);
        return (operations.Select(o => (OperationResponse)o), count);
    }
}