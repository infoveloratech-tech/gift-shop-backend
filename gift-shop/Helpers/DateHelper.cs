namespace gift_shop.Helpers;

public static class DateHelper
{
    /// <summary>
    /// Gets the start of the current day
    /// </summary>
    public static DateTime GetStartOfDay(DateTime date)
    {
        return date.Date;
    }

    /// <summary>
    /// Gets the end of the current day
    /// </summary>
    public static DateTime GetEndOfDay(DateTime date)
    {
        return date.Date.AddDays(1).AddTicks(-1);
    }

    /// <summary>
    /// Gets the start of the current month
    /// </summary>
    public static DateTime GetStartOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }

    /// <summary>
    /// Gets the end of the current month
    /// </summary>
    public static DateTime GetEndOfMonth(DateTime date)
    {
        var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
        return new DateTime(date.Year, date.Month, lastDay, 23, 59, 59, 999);
    }

    /// <summary>
    /// Gets the start of the current week
    /// </summary>
    public static DateTime GetStartOfWeek(DateTime date)
    {
        var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    /// <summary>
    /// Gets the end of the current week
    /// </summary>
    public static DateTime GetEndOfWeek(DateTime date)
    {
        return GetStartOfWeek(date).AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
    }

    /// <summary>
    /// Gets the start of the current year
    /// </summary>
    public static DateTime GetStartOfYear(DateTime date)
    {
        return new DateTime(date.Year, 1, 1);
    }

    /// <summary>
    /// Gets the end of the current year
    /// </summary>
    public static DateTime GetEndOfYear(DateTime date)
    {
        return new DateTime(date.Year, 12, 31, 23, 59, 59, 999);
    }

    /// <summary>
    /// Calculates age from birth date
    /// </summary>
    public static int GetAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    /// <summary>
    /// Creates a date range
    /// </summary>
    public static DateRange CreateDateRange(DateTime startDate, DateTime endDate)
    {
        return new DateRange
        {
            StartDate = startDate,
            EndDate = endDate,
            DaysInRange = (endDate - startDate).Days + 1
        };
    }

    /// <summary>
    /// Checks if date is in range
    /// </summary>
    public static bool IsDateInRange(DateTime date, DateTime startDate, DateTime endDate)
    {
        return date >= startDate && date <= endDate;
    }

    /// <summary>
    /// Converts DateTime to Unix timestamp
    /// </summary>
    public static long ToUnixTimestamp(DateTime date)
    {
        return (long)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    /// <summary>
    /// Converts Unix timestamp to DateTime
    /// </summary>
    public static DateTime FromUnixTimestamp(long timestamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
        return dateTime;
    }

    /// <summary>
    /// Gets human-readable time difference (e.g., "2 days ago")
    /// </summary>
    public static string GetTimeAgoString(DateTime date)
    {
        var timeSpan = DateTime.UtcNow - date;

        if (timeSpan.TotalSeconds < 60)
            return "Just now";
        else if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} minute(s) ago";
        else if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} hour(s) ago";
        else if (timeSpan.TotalDays < 7)
            return $"{(int)timeSpan.TotalDays} day(s) ago";
        else if (timeSpan.TotalDays < 30)
            return $"{(int)(timeSpan.TotalDays / 7)} week(s) ago";
        else if (timeSpan.TotalDays < 365)
            return $"{(int)(timeSpan.TotalDays / 30)} month(s) ago";
        else
            return $"{(int)(timeSpan.TotalDays / 365)} year(s) ago";
    }

    /// <summary>
    /// Formats a date to a standard format
    /// </summary>
    public static string FormatDate(DateTime date, string format = "yyyy-MM-dd")
    {
        return date.ToString(format);
    }

    /// <summary>
    /// Formats a date and time to a standard format
    /// </summary>
    public static string FormatDateTime(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return date.ToString(format);
    }

    /// <summary>
    /// Gets business days between two dates
    /// </summary>
    public static int GetBusinessDaysBetween(DateTime startDate, DateTime endDate)
    {
        int businessDays = 0;
        var current = startDate;

        while (current <= endDate)
        {
            if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
                businessDays++;

            current = current.AddDays(1);
        }

        return businessDays;
    }

    /// <summary>
    /// Next working day (excludes weekends)
    /// </summary>
    public static DateTime GetNextWorkingDay(DateTime date)
    {
        var next = date.AddDays(1);
        while (next.DayOfWeek == DayOfWeek.Saturday || next.DayOfWeek == DayOfWeek.Sunday)
            next = next.AddDays(1);

        return next;
    }
}

/// <summary>
/// Date range model
/// </summary>
public class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DaysInRange { get; set; }
}
