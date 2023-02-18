namespace EasyTrade.DTO.Abstractions;

public interface IDataRecorder<in TData>
{
    public void Record(TData data);
}