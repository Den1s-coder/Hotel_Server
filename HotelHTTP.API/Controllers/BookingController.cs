using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly IUserRepository _userRepository;
        
        public BookingController(BookingService bookingService, IUserRepository userRepository)
        {
            _bookingService = bookingService;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserBookings()
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var bookings = await _bookingService.GetUserBookingAsync(user.Id);
            return Ok(bookings);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingRequest booking)
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            if (user.Id == null)
                return Unauthorized();

            await _bookingService.AddAsync(booking, user.Id);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
