using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EasyTrade.Service.Services;

public class OptimisticLocker : ILocker
{
    private LockerConfiguration _config;
    public OptimisticLocker(IOptions<LockerConfiguration> config)
    {
        _config = config.Value;
    }
    public void Lock(Action func) // странное название для оптимистичного способа, но интересный способ обобщить оба подхода
    {
        // неплохое решение, но стоит попробовать Polly
        for (int i = 1; i <= _config.IterationCount; ++i) // обычно циклы от 0 :)
        {
            try
            {
                func();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (i == _config.IterationCount)
                    throw new ConcurrentWriteException(); // отлично!
                Thread.Sleep(_config.DelayMilliseconds); // await Task.Delay (+ переделка всего на асинк)
                // тут бы залогировать факт попытки
                continue;
            }

            break;
        }
    }
}