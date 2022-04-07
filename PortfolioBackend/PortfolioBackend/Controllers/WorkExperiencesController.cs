using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperiencesController : ControllerBase
    {
        private readonly IWorkExperienceRepository _workExperienceRepository;

        public WorkExperiencesController(IWorkExperienceRepository workExperienceRepository)
        {
            _workExperienceRepository = workExperienceRepository;
        }
        // GET: api/<WorkExperiencesController>
        [HttpGet]
        public ActionResult<List<WorkExperience>> Get()
        {
            return _workExperienceRepository.Get();
        }

        // GET api/<WorkExperiencesController>/5
        [HttpGet("{id}")]
        public ActionResult<WorkExperience> Get(string id)
        {
            var workExperience = _workExperienceRepository.Get(id);

            if(workExperience == null)
            {
                return NotFound($"Work Experience with Id = {id} not found");
            }
            return workExperience;
        }

        // POST api/<WorkExperiencesController>
        [HttpPost]
        public ActionResult<WorkExperience> Post([FromBody] WorkExperience workExperience)
        {
            _workExperienceRepository.Create(workExperience);

            return CreatedAtAction(nameof(Get), new { id = workExperience.Id}, workExperience);
        }

        // PUT api/<WorkExperiencesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] WorkExperience workExperience)
        {
            var existingWorkExperience= _workExperienceRepository.Get(id);

            if(existingWorkExperience == null)
            {
                return NotFound($"Work Experience with Id = {id} not found");
            }

            _workExperienceRepository.Update(id, workExperience);

            return NoContent();
        }

        // DELETE api/<WorkExperiencesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var workExperience = _workExperienceRepository.Get(id);

            if (workExperience == null)
            {
                return NotFound($"Work Experience with Id = {id} not found");
            }

            _workExperienceRepository.Delete(workExperience.Id);

            return Ok($"Work Experience with Id = {id} deleted");
        }
    }
}
