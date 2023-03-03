using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class PessimisticLocker : ILocker
{
    public void ConcurrentExecute(Action func, object lockObject)
    {
        lock (lockObject)
        {
            func();
        }
    }
}