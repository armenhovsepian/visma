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

        #region Entity Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(p => p.Customer)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Guid);
        }
        #endregion
    }
}
