using EasyTrade.DTO.Abstractions;

namespace EasyTrade.Service.Services;

public class PessimisticLocker : ILocker
{
    public void Lock(Action func)
    {
        var locker = new object(); 
        // будет узким местом для вообще всех операций с балансами с любыми валютами
        // возможно есть лучшее решение?
        lock (locker)
        {
            func();
            Thread.Sleep(TimeSpan.FromSeconds(15)); // для проверки локера
        }
    }
}