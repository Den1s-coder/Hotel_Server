using Hotel.API.Requests;
using Hotel.API.Service;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                Image = $"/images/{fileName}"
            };

            await _roomService.AddRoomAsync(room);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return Ok();
        }
    }
}
