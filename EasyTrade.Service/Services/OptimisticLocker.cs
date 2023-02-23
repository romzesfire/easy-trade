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
    public void Lock(Action func)
    {
        for (int i = 1; i <= _config.IterationCount; ++i)
        {
            try
            {
                func();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (i == _config.IterationCount)
                    throw new ConcurrentWriteException();
                Thread.Sleep(_config.DelayMilliseconds);
                continue;
            }

            break;
        }
    }
}