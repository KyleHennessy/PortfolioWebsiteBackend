using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        // GET: api/<MessagesController>
        [HttpGet]
        public ActionResult<List<Message>> Get()
        {
            return _messageRepository.Get();
        }

        // GET api/<MessagesController>/5
        [HttpGet("{id}")]
        public ActionResult<Message> Get(string id)
        {
            var message = _messageRepository.Get(id);

            if (message == null)
            {
                return NotFound($"Message with Id = {id} not found");
            }

            return message;
        }

        // POST api/<MessagesController>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Message> Post([FromBody] Message message)
        {
            _messageRepository.Create(message);

            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }


        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var message = _messageRepository.Get(id);

            if (message == null)
            {
                return NotFound($"Message with Id = {id} not found");
            }

            _messageRepository.Delete(message.Id);

            return Ok($"Massage with Id = {id} deleted");
        }
    }
}
