using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public ActionResult<Admin> Get(string id)
        {
            var admin = _adminRepository.Get(id);

            if (admin == null)
            {
                return NotFound($"Admin not found");
            }

            return admin;
        }

    }
}
