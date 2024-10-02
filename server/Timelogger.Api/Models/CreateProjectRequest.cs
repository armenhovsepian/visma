using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Api.Models
{
    public class CreateProjectRequest : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Deadline < DateTime.UtcNow)
            {
                yield return new ValidationResult(
                    "Project deadline cannot be in the past.",
                    new[] { nameof(Deadline) });
            }
        }
    }
}
