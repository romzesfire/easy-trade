using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class PessimisticLocker : ILocker
{
    public void ConcurrentExecute(Action func)
    {
        var locker = new object();
        lock (locker)
        {
            func();
        }
    }
}