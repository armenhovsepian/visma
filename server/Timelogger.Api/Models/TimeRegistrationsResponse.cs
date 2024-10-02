using System.Collections.Generic;
using System.Linq;
using Timelogger.DTOs;
using Timelogger.Helpers;

namespace Timelogger.Api.Models
{
    public class TimeRegistrationsResponse
    {
        public List<TimeRegistrationDto> TimeRegistrations { get; set; }
        public string TotalDuration => DateTimeHelpers.ConvertToISO8601(TimeRegistrations.Sum(tr => tr.DurationIsSeconds));
    }
}
