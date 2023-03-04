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
    public OperationResponse GetOperation(int id)
    {
        return (OperationResponse)_balanceRepository.GetAll().First(b=>b.Id == id);
    }

    public (IEnumerable<OperationResponse>, int) GetOperations(int limit, int offset)
    {
        var (operations, count) = _balanceRepository.GetLimited(limit, offset);
        return (operations.Select(o => (OperationResponse)o), count);
    }
}