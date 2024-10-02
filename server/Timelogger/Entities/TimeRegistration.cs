using System;

namespace Timelogger.Entities
{
    public class TimeRegistration
    {
        public TimeRegistration(int projectId, DateTime start, DateTime end)
        {
            ProjectId = projectId;
            Guid = Guid.NewGuid();
            Start = start;
            End = end;
        }

        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public int ProjectId { get; private set; }
        public Project Project { get; private set; }
    }
}
