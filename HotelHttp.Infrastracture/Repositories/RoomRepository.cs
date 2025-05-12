using Hotel.Domain.Entitys;
using Hotel.Domain.Interfaces;
using Hotel.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastracture.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDBContext _context;

        public RoomRepository(HotelDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public async Task AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
            if (room != null) 
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
