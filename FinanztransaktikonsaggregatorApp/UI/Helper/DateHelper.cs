namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class DateHelper
{
    public static  string TimeSpan()
    {
        DateTime now = DateTime.Now;

        DateTime start = new DateTime(now.Year, now.Month, 1);
        DateTime end = start.AddMonths(1).AddDays(-1);

        string timespan = $"{start:dd.MM} - {end:dd.MM}";
        return timespan;

    }
    public static  int CurrentMonth()
    {
        var now = DateTime.Now;
        int month = now.Month;

        return month;
    }

    public static int CurrentYear()
    {
        var now = DateTime.Now;
        int year = now.Year;

        return year;
    }
}
