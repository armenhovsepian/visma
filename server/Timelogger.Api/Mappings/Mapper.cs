using System.Linq;
using Timelogger.Api.Models;
using Timelogger.Entities;

namespace Timelogger.Api.Mappings
{
    public static class Mapper
    {
        public static ProjectModel ToProjectModel(this Project project)
        {
            return new ProjectModel
            {
                Guid = project.Guid,
                Name = project.Name,
                Description = project.Description,
                Color = project.Color,
                Deadline = project.Deadline,
                CompletedDate = project.CompletedDate,
                TimeRegistrations = project.TimeRegistrations
                                    .Select(timeLog => timeLog.ToTimeRegistrationModel())
                                    .ToList()
            };
        }

        public static TimeRegistrationModel ToTimeRegistrationModel(this TimeRegistration timeRegistration)
        {
            return new TimeRegistrationModel
            {
                ProjectGuid = timeRegistration.Project.Guid,
                Start = timeRegistration.Start,
                End = timeRegistration.End
            };
        }

    }
}
