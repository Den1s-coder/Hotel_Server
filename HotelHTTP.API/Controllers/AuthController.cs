using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Auth;
using Hotel.Domain.Interfaces.Repo;
using Hotel.Infrastracture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly ITokenService _tokenSerive;
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(AuthService authService, ITokenService tokenSerive, IPasswordHasher passwordHasher)
        {
            _authService = authService;
            _tokenSerive = tokenSerive;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _authService.Register(request);

            return Ok("Користувача зареєстровано");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string token;

            try
            {
                token = await _authService.Login(request);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

            Response.Cookies.Append("wery_good_cookies", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout() 
        {
            Response.Cookies.Delete("wery_good_cookies", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        }
    }
}
