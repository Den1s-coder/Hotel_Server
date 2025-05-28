using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllRoomsAsync();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);

            return Ok(room);
        }

        [HttpPost]
        [Authorize]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> Create([FromForm] RoomRequest model)
        {
            if (model.Image == null || model.Image.Length == 0)
                return BadRequest("Зображення не завантажено.");

            var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var room = new Room
            {
                Number = model.Number,
                IsAvailable = true,
                PricePerNight = model.PricePerNight,
                Type = model.Type,
                Capacity = model.Capacity,
                Image = $"/images/{fileName}"
            };

            await _roomService.AddRoomAsync(room);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> Update(int id, [FromForm] RoomRequest model)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
                return NotFound("Кімната не знайдена.");

            if (model.Image != null && model.Image.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                room.Image = $"/images/{fileName}";
            }

            room.Number = model.Number;
            room.Type = model.Type;
            room.PricePerNight = model.PricePerNight;
            room.Capacity = model.Capacity;

            await _roomService.UpdateRoomAsync(room);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoomAsync(id);

            return Ok();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate, [FromQuery] int maxPrice, [FromQuery] int capacity)
        {
            var availableRooms = await _roomService.GetActivityRoomsAsync(checkInDate, checkOutDate, maxPrice, capacity);

            return Ok(availableRooms);
        }

        [HttpPost("update-prices")]
        [Authorize]
        public async Task<IActionResult> ChangePriceByType([FromForm] UpdatePriceRequest request)
        {
            await _roomService.UpdatePriceByTypeAsync(request.Type, request.NewPrice);

            return Ok();
        }
    }
}
