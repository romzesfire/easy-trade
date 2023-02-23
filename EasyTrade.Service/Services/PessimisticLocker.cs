using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class PessimisticLocker : ILocker
{
    public void Lock(Action func)
    {
        var locker = new object();
        lock (locker)
        {
            func();
        }
    }
}