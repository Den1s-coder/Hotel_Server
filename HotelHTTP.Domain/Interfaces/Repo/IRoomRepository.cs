using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Repo
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task DeleteAsync(int id);
    }
}
