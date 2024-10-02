using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Api.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            TimeRegistrations = new List<TimeRegistrationModel>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Guid Guid { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public List<TimeRegistrationModel> TimeRegistrations { get; set; }
    }
}
