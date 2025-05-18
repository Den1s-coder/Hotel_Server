using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Auth;
using Hotel.Domain.Interfaces.Repo;
using Hotel.Infrastracture;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly TokenService _tokenSerive;
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(IUserRepository userRepo, TokenService tokenSerive)
        {
            _userRepo = userRepo;
            _tokenSerive = tokenSerive;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existing = await _userRepo.GetByEmail(request.Email);
            if (existing != null)
            {
                return Conflict("Користувача з такою поштою не існує.");
            }

            var hashedPassword = _passwordHasher.Generate(request.Password);
            var user = Hotel.Domain.Entities.User.Create(Guid.NewGuid(), request.Name, hashedPassword, request.Email);

            await _userRepo.Add(user);

            return Ok("Користувача зареєстровано");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepo.GetByEmail(request.Email);
            if(user == null)
            {
                return Unauthorized("Невірна пошта або пароль.");
            }
            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Невірна пошта або пароль.");
            }

            var token = _tokenSerive.GenerateToken(user.Email);
            return Ok(new { token });
        }
    }
}
