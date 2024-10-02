using System;

namespace Timelogger.Helpers
{
    public static class DateTimeHelpers
    {
        public static bool IsDateRangeValid(DateTime start, DateTime end)
        {
            TimeSpan difference = end - start;
            int minutes = (int)difference.TotalMinutes;
            return minutes >= 30;
        }
    }
}
