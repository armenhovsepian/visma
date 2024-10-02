using System;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Api.Models
{
    public class CreateProjectRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime Deadline { get; set; }
    }
}
