using Timelogger.DTOs;
using Timelogger.Entities;

namespace Timelogger.Api.Mappings
{
    public static class Mapper
    {
        public static ProjectDto ToProjectDto(this Project project)
        {
            return new ProjectDto
            {
                Guid = project.Guid,
                Name = project.Name,
                Customer = project.Customer,
                Deadline = project.Deadline,
                CreatedDate = project.CreatedDate,
                IsCompleted = project.CompletedDate != null
            };
        }

        public static TimeRegistrationDto ToTimeRegistrationDto(this TimeRegistration timeRegistration)
        {
            return new TimeRegistrationDto
            {
                Start = timeRegistration.Start,
                End = timeRegistration.End
            };
        }

    }
}
