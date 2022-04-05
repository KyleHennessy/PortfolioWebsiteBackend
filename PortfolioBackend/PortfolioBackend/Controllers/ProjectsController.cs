using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        // GET: api/<ProjectsController>
        [HttpGet]
        public ActionResult<List<Project>> Get()
        {
            return projectService.Get();
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public ActionResult<Project> Get(string id)
        {
            var project = projectService.Get(id);

            if(project == null)
            {
                return NotFound($"Project with Id = {id} not found");
            }

            return project;
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public ActionResult<Project> Post([FromBody] Project project)
        {
            projectService.Create(project);

            return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Project project)
        {
            var existingProject = projectService.Get(id);

            if(existingProject == null)
            {
                return NotFound($"Project with Id = {id} not found");
            }

            projectService.Update(id, project);

            return NoContent();
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var project = projectService.Get(id);

            if(project == null)
            {
                return NotFound($"Project with Id = {id} not found");
            }

            projectService.Delete(project.Id);

            return Ok($"Project with Id = {id} deleted");
        }

    }
}
