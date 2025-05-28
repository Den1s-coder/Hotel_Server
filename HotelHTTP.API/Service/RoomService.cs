using Hotel.Domain.Interfaces.Repo;
using Hotel.Domain.Entities;

namespace Hotel.API.Service
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task AddRoomAsync(Room room)
        {
            await _roomRepository.AddAsync(room);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Room>> GetActivityRoomsAsync(DateTime checkIn,DateTime checkOut, int? maxPrice, int? capacity)
        {
            return await _roomRepository.GetActivityAsync(checkIn, checkOut, maxPrice, capacity);
        }

        public async Task UpdatePriceByTypeAsync(string type, decimal newPrice)
        {
            await _roomRepository.UpdatePriceByTypeAsync(type, newPrice);
        }
    }
}
