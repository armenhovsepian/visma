using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Api.Mappings;
using Timelogger.Api.Models;
using Timelogger.Entities;
using Timelogger.Interfaces.Repositories;

namespace Timelogger.Api.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectRepository projectRepository,
            ILogger<ProjectsController> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        // GET api/projects
        [HttpGet]
        [Route("api/projects")]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] bool? sorted, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetProjects(pageNumber ?? 1, pageSize ?? 10, sorted, cancellationToken);
            return Ok(projects);
        }

        [HttpGet]
        [Route("api/projects/{projectGuid:Guid}")]
        public async Task<IActionResult> Get(Guid projectGuid, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            if (project == null)
            {
                _logger.LogInformation($"A project with the specified GUID '{projectGuid}' cannot be found.");
                return NotFound();
            }

            return Ok(project.ToProjectDto());
        }

        [HttpGet]
        [Route("api/projects/{projectGuid:Guid}/timeRegistrations")]
        public async Task<IActionResult> TimeRegistrations(Guid projectGuid, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var timeRegistrations = await _projectRepository.GetTimeRegistrations(projectGuid, pageNumber ?? 1, pageSize ?? 10, cancellationToken);

            return Ok(new TimeRegistrationsResponse
            {
                TimeRegistrations = timeRegistrations.Select(tr => tr.ToTimeRegistrationDto()).ToList()
            });
        }

        /// <summary>
        /// Create a new Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/projects/create")]
        public async Task<IActionResult> Create(CreateProjectRequest project)
        {
            var createdProject = new Project(project.Name, project.Deadline);

            await _projectRepository.Create(createdProject);
            await _projectRepository.SaveChangesAsync();

            return Created($"api/projects/{createdProject.Guid}", createdProject);
        }

        [HttpPost]
        [Route("api/projects/{projectGuid:Guid}/registerTime")]
        public async Task<IActionResult> RegisterTime(Guid projectGuid, CreateTimeRegistration timeRegistration, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            var result = project.RegisterTime(timeRegistration.Start, timeRegistration.End);
            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            await _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("api/projects/{projectGuid:Guid}/complete")]
        public async Task<IActionResult> Complete(Guid projectGuid, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            if (project == null)
            {
                _logger.LogInformation($"A project with the specified GUID '{projectGuid}' cannot be found.");
                return NotFound();
            }

            project.Complete();
            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
