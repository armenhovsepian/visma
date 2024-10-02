using System;

namespace Timelogger.Helpers
{
    public static class DateTimeHelpers
    {
        public static bool IsDateRangeValid(DateTime start, DateTime end)
        {
            TimeSpan difference = end - start;
            int minutes = (int)difference.TotalMinutes;
            return minutes >= Constants.RegisterationTimeDuration;
        }

        public static int GetDifferenceInSeconds(DateTime start, DateTime end)
        {
            TimeSpan difference = end - start;
            return (int)difference.TotalSeconds;
        }

        public static string ConvertToISO8601(int totalSeconds)
        {
            // Calculate hours, minutes, and seconds
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            // Build the ISO 8601 duration string
            string isoDuration = "PT";
            if (hours > 0) isoDuration += $"{hours}H";
            if (minutes > 0) isoDuration += $"{minutes}M";
            if (seconds > 0 || (hours == 0 && minutes == 0)) isoDuration += $"{seconds}S";  // Ensure at least seconds is added

            return isoDuration;
        }
    }
}
