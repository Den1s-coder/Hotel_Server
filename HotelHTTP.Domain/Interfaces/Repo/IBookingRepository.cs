using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces.Repo
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId);
        Task DeleteAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
    }
}