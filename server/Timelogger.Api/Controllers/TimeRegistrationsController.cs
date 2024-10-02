using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Data;
using Timelogger.Entities;

namespace TimeLoggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRegistrationsController : ControllerBase
    {
        private readonly TimeLoggerContext _context;

        public TimeRegistrationsController(TimeLoggerContext context)
        {
            _context = context;
        }

        // GET: api/timeregistrations/project/5
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<TimeRegistration>>> GetTimeRegistrationsForProject(int projectId)
        {
            return await _context.TimeRegistrations.Where(t => t.ProjectId == projectId).ToListAsync();
        }

        //// POST: api/timeregistrations
        //[HttpPost]
        //public async Task<ActionResult<TimeRegistration>> LogTime(TimeRegistration timeRegistration)
        //{
        //    var project = await _context.Projects.FindAsync(timeRegistration.ProjectId);

        //    if (project == null || project.IsCompleted)
        //    {
        //        return BadRequest("Cannot log time to a non-existent or completed project.");
        //    }

        //    if (timeRegistration.HoursSpent < 0.5)
        //    {
        //        return BadRequest("Time registration must be 30 minutes or longer.");
        //    }

        //    _context.TimeRegistrations.Add(timeRegistration);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTimeRegistrationsForProject", new { projectId = timeRegistration.ProjectId }, timeRegistration);
        //}
    }
}
