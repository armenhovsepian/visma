using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public interface IProjectRepository
    {
        Task Create(Project project);
        Task<Project> Get(Guid projectGuid, CancellationToken cancellationToken = default);
        Task<List<Project>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task Update(Project project);
    }
}