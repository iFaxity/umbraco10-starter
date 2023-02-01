using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Guttew.Umbraco.Extensions;

public static class DateTimeExtensions
{
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

    /// <summary>
    ///
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static DateTime? GetEndOfMonth(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;

        return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static DateTime? GetStartOfMonth(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;
        return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0, date.Kind);
    }

    /// <summary>
    /// Gets the first date (monday) of the selected week.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static DateTime? GetFirstDateOfWeek(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;

        // get the week of the years first day
        var jan1 = new DateTime(date.Year, 1, 1);
        var week = GetWeek(date);

        // get the date of the first monday this year
        var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
        var firstMonday = jan1.AddDays(daysOffset);

        // get the first week of the year based on the monday
        var firstWeek = GetWeek(firstMonday);
        if (firstWeek <= 1)
            week -= 1;

        // return date of the monday of the specified week
        return firstMonday.AddDays(week.Value * 7);
    }

    /// <summary>
    /// Gets the week of the year of the current date (1-53).
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>Week from the date (1-53).</returns>
    [return: NotNullIfNotNull("dateTime")]
    public static int? GetWeek(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;

        // get the week from the calender method
        var calender = CultureInfo.CurrentCulture.Calendar;
        var week = calender.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        // if week 53 and weekday is before thursday, its week 1
        if (week == 53 && new DateTime(date.Year, date.Month, 31).DayOfWeek < DayOfWeek.Thursday)
            return 1;

        // return the week
        return week;
    }

    /// <summary>
    /// Returns a Unix Epoch time if given a value, and null otherwise.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static long? ToEpochTime(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var delta = dateTime.Value - Epoch;
        return (long)delta.TotalSeconds;
    }

    /// <summary>
    /// Returns end of the day datetime "dd.mm.yyyy 23:59:59"
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> source.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static DateTime? EndOfDay(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;

        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
    }

    /// <summary>
    /// Returns beginning of the day datetime "dd.mm.yyyy 00:00:00"
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> source.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("dateTime")]
    public static DateTime? BeginningOfDay(this DateTime? dateTime)
    {
        if (dateTime is null)
            return null;

        var date = dateTime.Value;
        return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
    }

    /// <summary>
    /// Checks if date is today (Should be in UTC).
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> to check</param>
    /// <returns></returns>
    public static bool IsToday([NotNullWhen(false)] this DateTime? dateTime)
    {
        return dateTime?.Date == DateTime.UtcNow.Date;
    }

    /// <summary>
    /// Checks if date is tomorrow (Should be in UTC).
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> to check</param>
    /// <returns></returns>
    public static bool IsTomorrow([NotNullWhen(false)] this DateTime? date)
    {
        return DateTime.UtcNow.Date == date?.Date.AddDays(-1);
    }

    /// <summary>
    /// Checks if date is yesterday (Should be in UTC).
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> to check</param>
    /// <returns></returns>
    public static bool IsYesterday([NotNullWhen(false)] this DateTime? date)
    {
        return DateTime.UtcNow.Date == date?.Date.AddDays(1);
    }

    /// <summary>
    /// Converts datetime to timestamp.
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> to convert</param>
    /// <returns>Timestamp.</returns>
    [return: NotNullIfNotNull("dateTime")]
    public static int? ToTimestamp(this DateTime? dateTime)
    {
        return (int?)dateTime?.ToUniversalTime().Subtract(Epoch).TotalSeconds;
    }
}
