using Hotel.Domain.Interfaces.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrentUser() 
        {
            var email = User.Identity?.Name;
            if (email == null) 
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) 
            {
                return NotFound();
            }

            return Ok(user.Email);
        }
    }
}
