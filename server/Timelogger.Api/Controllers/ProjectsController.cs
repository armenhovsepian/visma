using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Api.Mappings;
using Timelogger.Api.Models;
using Timelogger.Entities;
using Timelogger.Repositories;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [HttpGet]
        [Route("hello-world")]
        public string HelloWorld()
        {
            return "Hello Back!";
        }

        // GET api/projects
        [HttpGet]
        [Route("api/projects/{pageNumber:int?}/{pageSize:int?}")]
        public async Task<IActionResult> GetAll(uint? pageNumber, uint? pageSize, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAll((int)(pageNumber ?? 1), (int)(pageSize ?? 10), cancellationToken);
            return Ok(projects.Select(project => project.ToProjectModel()).ToList());
        }

        [HttpGet]
        [Route("api/projects/{projectGuid: Guid}")]
        public async Task<IActionResult> Get(Guid projectGuid, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.ToProjectModel());
        }

        [HttpGet]
        [Route("api/projects/{projectGuid:Guid}/timeregistrations/{pageNumber:int?}/{pageSize:int?}")]
        public async Task<IActionResult> TimeRegistrations(Guid projectGuid, int? pageNumber, int? pageSize, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.TimeRegistrations.Select(timeRegistration => timeRegistration.ToTimeRegistrationModel()).ToList());
        }

        [HttpPost]
        [Route("api/projects/create")]
        public async Task<IActionResult> Create(ProjectModel project)
        {
            var createdProject = new Project(project.Name, project.Description, project.Color, project.Deadline);

            await _projectRepository.Create(createdProject);
            await _projectRepository.SaveChangesAsync();

            return Created($"api/projects/{createdProject.Guid}", createdProject);
        }

        [HttpGet]
        [Route("api/projects/{projectGuid: Guid}/registerTime")]
        public async Task<IActionResult> RegisterTime(Guid projectGuid, TimeRegistrationModel timeRegistration, CancellationToken cancellationToken)
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


        // PUT: api/projects/5
        [HttpPut]
        [Route("api/projects/{projectGuid: Guid}/complete")]
        public async Task<IActionResult> Complete(Guid projectGuid, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.Get(projectGuid, cancellationToken);
            if (project == null)
            {
                return NotFound();
            }

            project.Complete();
            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
