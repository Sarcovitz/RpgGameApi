using RpgGame.Providers.Interfaces;
using System;

namespace RpgGame.Providers;

public class TimeHelper : ITimeHelper
{
    public DateTime GetCurrentLocalTime()
        => DateTime.Now;

    public long GetCurrentUnixTimestamp()
        => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

    public DateTime GetCurrentUtcTime()
        => DateTime.UtcNow;

    public long GetUnixTimestampFromDate(DateTime dateTime)
        => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}
