using EasyTrade.Service.Model.ResponseModels;

namespace EasyTrade.DTO.Abstractions;

public interface IOperationProvider
{
    public Task<OperationResponse> GetOperation(int id, Guid userId);
    public (IEnumerable<OperationResponse>, int) GetOperations(int limit, int offset, Guid userId);
}