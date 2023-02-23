using EasyTrade.DTO.Model;

namespace EasyTrade.Service.Configuration;

public class LockerConfiguration
{
    public LockerType Type { get; set; }
    public int DelayMilliseconds { get; set; }
    public int IterationCount { get; set; }
}