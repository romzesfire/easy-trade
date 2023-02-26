namespace EasyTrade.DTO.Abstractions;

public interface ILocker
{
    public void ConcurrentExecute(Action func);
}