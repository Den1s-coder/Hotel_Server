using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Repo
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room updatedRoom);
        Task DeleteAsync(int id);
        Task<IEnumerable<Room>> GetActivityAsync(DateTime checkInDate, DateTime checkOutDate, int? maxPrice, int? capacity);
        Task UpdatePriceByTypeAsync(string type, decimal newPrice);
    }
}
