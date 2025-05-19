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
        public async Task<IActionResult> Create([FromBody] Room room)
        {
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
