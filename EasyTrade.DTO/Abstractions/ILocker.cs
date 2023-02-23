namespace EasyTrade.DTO.Abstractions;

public interface ILocker
{
    public void Lock(Action func);
}