using System;

namespace Timelogger.DTOs
{
    public class ProjectDto
    {
        public string Name { get; set; }
        public string Customer { get; set; }
        public Guid Guid { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
