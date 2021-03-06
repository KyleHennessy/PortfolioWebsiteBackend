using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        // GET: api/<SkillsController>
        [HttpGet]
        public ActionResult<List<Skill>> Get()
        {
            return _skillRepository.Get();
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        public ActionResult<Skill> Get(string id)
        {
            var skill = _skillRepository.Get(id);

            if(skill == null)
            {
                return NotFound($"Skill with Id = {id} not found");
            }

            return skill;
        }

        // POST api/<SkillsController>
        [HttpPost]
        public ActionResult<Skill> Post([FromBody] Skill skill)
        {
            _skillRepository.Create(skill);

            return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Skill skill)
        {
            var existingSkill= _skillRepository.Get(id);

            if(existingSkill == null)
            {
                return NotFound($"Skill with Id = {id} not found");
            }

            _skillRepository.Update(id, skill);

            return NoContent();
        }

        // DELETE api/<SkillsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var skill = _skillRepository.Get(id);

            if(skill == null)
            {
                return NotFound($"Skill with Id = {id} not found");
            }

            _skillRepository.Delete(skill.Id);

            return Ok($"Skill with Id = {id} deleted");
        }
    }
}
