using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger.Data
{
    public class TimeLoggerContext : DbContext
    {
        public TimeLoggerContext(DbContextOptions<TimeLoggerContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeRegistration> TimeRegistrations { get; set; }
    }
}
