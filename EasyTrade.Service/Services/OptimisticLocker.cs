using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace EasyTrade.Service.Services;

public class OptimisticLocker : ILocker
{
    private RetryPolicy _retryPolicy;
    public OptimisticLocker(IOptions<LockerConfiguration> config)
    {
        _retryPolicy = Policy.Handle<ConcurrentWriteException>()
            .WaitAndRetry(config.Value.IterationCount,
                retry => TimeSpan.FromMilliseconds(config.Value.DelayMilliseconds));
    }
    public void Lock(Action func)
    {
        _retryPolicy.Execute(func);
    }
}