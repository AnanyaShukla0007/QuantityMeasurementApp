namespace Shared.Common.Helpers;

public static class DateTimeHelper
{
    public static DateTime UtcNow() => DateTime.UtcNow;
    public static string UtcNowString() => DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
}