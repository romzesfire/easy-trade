using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IOperationRecorder : IDataRecorder<UpdateBalanceModel>
{
    public Task Record(IEnumerable<Operation> operations, Guid userId = default);
}