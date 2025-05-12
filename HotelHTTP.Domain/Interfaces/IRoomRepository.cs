using Hotel.Domain.Entitys;

namespace Hotel.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task DeleteAsync(int id);
    }
}
