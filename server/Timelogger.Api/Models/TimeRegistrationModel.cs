using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Timelogger.Helpers;

namespace Timelogger.Api.Models
{
    public class TimeRegistrationModel : IValidatableObject
    {
        public Guid ProjectGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Start > End)
            {
                yield return new ValidationResult(
                    $"Start date ({Start}) cannot be later than the end date ({End}).",
                    new[] { nameof(Start) });
            }


            if (DateTimeHelpers.IsDateRangeValid(Start, End))
            {
                yield return new ValidationResult(
                    $"The date range is invalid (less than 30 minutes).",
                    new[] { nameof(End) });
            }
        }


    }
}
