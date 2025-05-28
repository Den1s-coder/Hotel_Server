using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
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

        public async Task UpdateAsync(Room updatedRoom)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == updatedRoom.Id);

            if (existingRoom != null)
            {
                existingRoom.Number = updatedRoom.Number;
                existingRoom.Type = updatedRoom.Type;
                existingRoom.PricePerNight = updatedRoom.PricePerNight;
                existingRoom.Image = updatedRoom.Image;
                existingRoom.IsAvailable = updatedRoom.IsAvailable;

                await _context.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<Room>> GetActivityAsync(DateTime checkInDate, DateTime checkOutDate, int? maxPrice, int? capacity)
        {
            var query = _context.Rooms.AsQueryable();

            query = query
                .Where(room =>!_context.Bookings
                .Any(b =>b.RoomId == room.Id && (checkInDate < b.CheckOutDate && checkOutDate > b.CheckInDate))
            );

            if (maxPrice.HasValue)
            {
                if (maxPrice != 0)
                {
                    query = query.Where(room => room.PricePerNight <= maxPrice.Value);
                }
            }
            
            if (capacity.HasValue)
            {
                query = query.Where(room => room.Capacity >= capacity.Value);
            }

            var rooms = await query.ToListAsync();
            return rooms;
        }

        public async Task UpdatePriceByTypeAsync(string type, decimal newPrice)
        {
            var rooms = await _context.Rooms.Where(r => r.Type == type).ToListAsync();
            foreach (var room in rooms)
            {
                room.PricePerNight = newPrice;
            }
            await _context.SaveChangesAsync();
        }
    }
}
