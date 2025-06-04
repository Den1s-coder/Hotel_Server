using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize]
        [HttpGet("bookings-per-day")]
        public async Task<IActionResult> GetBookingsPerDay([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var bookings = await _bookingService.GetAllAsync();
            var result = bookings.Where(b => b.CheckInDate <= endDate && b.CheckOutDate >= startDate)
                .SelectMany(b =>
                    Enumerable.Range(0, (b.CheckOutDate - b.CheckInDate).Days)
                        .Select(offset => b.CheckInDate.AddDays(offset))
                )
                .Where(date => date >= startDate && date <= endDate)
                .GroupBy(date => date.Date)
                .Select(g => new { date = g.Key, count = g.Count() })
                .OrderBy(x => x.date)
                .ToList();

            return Ok(result);
        }
    }
}
