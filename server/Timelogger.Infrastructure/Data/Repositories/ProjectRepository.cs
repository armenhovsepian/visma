using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.DTOs;
using Timelogger.Entities;
using Timelogger.Interfaces.Repositories;

namespace Timelogger.Infrastructure.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TimeLoggerContext _context;
        public ProjectRepository(TimeLoggerContext context)
        {
            _context = context;
        }


        public async Task<Project> Get(Guid projectGuid, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Include(project => project.TimeRegistrations)
                .SingleOrDefaultAsync(project => project.Guid == projectGuid, cancellationToken);
        }

        public async Task<List<ProjectDto>> GetProjects(int pageNumber, int pageSize, bool? sorted, CancellationToken cancellationToken = default)
        {
            IOrderedQueryable<Project> projects = sorted switch
            {
                true => _context.Projects.OrderByDescending(p => p.Deadline),
                false => _context.Projects.OrderBy(p => p.Deadline),
                _ => _context.Projects.OrderBy(p => p.Id),
            };

            return await projects.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProjectDto
                {
                    Guid = p.Guid,
                    Name = p.Name,
                    Customer = p.Customer,
                    Deadline = p.Deadline,
                    CreatedDate = p.CreatedDate,
                    IsCompleted = p.CompletedDate != null
                })
                .ToListAsync(cancellationToken);
        }

        public async Task Create(Project project)
        {
            _context.Projects.Add(project);
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TimeRegistration>> GetTimeRegistrations(Guid projectGuid, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Where(project => project.Guid == projectGuid)
                .SelectMany(project => project.TimeRegistrations)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
