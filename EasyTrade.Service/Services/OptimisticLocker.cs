using EasyTrade.DTO.Abstractions;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace EasyTrade.Service.Services;

public class OptimisticLocker : ILocker
{
    private RetryPolicy _retryPolicy;
    public OptimisticLocker(IOptions<LockerConfiguration> config)
    {
        var delay = Backoff
            .DecorrelatedJitterBackoffV2(TimeSpan.FromMilliseconds(config.Value.DelayMilliseconds), 
                config.Value.IterationCount);
        
        _retryPolicy = Policy.Handle<DbUpdateConcurrencyException>()
            .WaitAndRetry(delay);
        
    }
    public void ConcurrentExecute(Action func, object lockObject)
    {
        var result = _retryPolicy.ExecuteAndCapture(func);
        if (result.FinalException != null)
        {
            if (result.FinalException is DbUpdateConcurrencyException)
            {
                throw new ConcurrentWriteException();
            }
            else
            {
                throw result.FinalException;
            }
        }
    }
}