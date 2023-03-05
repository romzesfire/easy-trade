namespace EasyTrade.DTO.Abstractions;

public interface ILocker
{
    public void ConcurrentExecute(Action func, object lockObject);
    public Task ConcurrentExecuteAsync(Action func, object lockObject);
}