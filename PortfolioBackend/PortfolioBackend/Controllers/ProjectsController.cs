using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services;
using PortfolioBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectService)
        {
            _projectRepository = projectService;
        }
        // GET: api/<ProjectsController>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Project>> Get()
        {
            return _projectRepository.Get();
        }

        // GET api/<ProjectsController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Project> Get(string id)
        {
            var project = _projectRepository.Get(id);

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
            _projectRepository.Create(project);

            return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Project project)
        {
            var existingProject = _projectRepository.Get(id);

            if(existingProject == null)
            {
                return NotFound($"Project with Id = {id} not found");
            }

            _projectRepository.Update(id, project);

            return NoContent();
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var project = _projectRepository.Get(id);

            if(project == null)
            {
                return NotFound($"Project with Id = {id} not found");
            }

            _projectRepository.Delete(project.Id);

            return Ok($"Project with Id = {id} deleted");
        }

    }
}
