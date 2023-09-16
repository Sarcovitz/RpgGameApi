namespace RpgGame.Providers.Interfaces;

public interface ITimeHelper
{
    DateTime GetCurrentLocalTime();
    DateTime GetCurrentUtcTime();
    long GetUnixTimestampFromDate(DateTime dateTime);
    long GetCurrentUnixTimestamp();
}
