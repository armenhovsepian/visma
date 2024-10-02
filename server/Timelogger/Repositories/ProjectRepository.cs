using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Data;
using Timelogger.Entities;

namespace Timelogger.Repositories
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

        public async Task<List<Project>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
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
    }
}
