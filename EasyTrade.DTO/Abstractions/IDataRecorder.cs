namespace EasyTrade.DTO.Abstractions;

public interface IDataRecorder<in TData>
{
    public Task Record(TData data, Guid userId = default);
}