using EasyTrade.Domain.Model;
using EasyTrade.DTO.Model;

namespace EasyTrade.DTO.Abstractions;

public interface IOperationRecorder : IDataRecorder<UpdateBalanceModel>
{
    public void Record(IEnumerable<Operation> operations);
}