using System;
using Timelogger.Helpers;

namespace Timelogger.DTOs
{
    public class TimeRegistrationDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int DurationIsSeconds => DateTimeHelpers.GetDifferenceInSeconds(Start, End);
    }
}
