namespace ToDoApp.Helpers;

internal static class DateTimeHelper
{
    public static DateTime GetMondayDate(DateTime? dateTime)
    {
        var today = dateTime ?? DateTime.Today;
        var daysToSubtract = (int)today.DayOfWeek - (int)DayOfWeek.Monday;

        if (daysToSubtract < 0) 
        {
            daysToSubtract += 7;
        }

        return today.AddDays(-daysToSubtract);
    }
}