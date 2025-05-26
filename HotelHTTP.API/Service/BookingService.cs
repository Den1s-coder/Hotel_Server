using Hotel.API.Requests;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
using Hotel.Infrastracture.Data;

namespace Hotel.API.Service
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _bookingRepository.GetAllAsync();
        } 

        public async Task<IEnumerable<Booking?>> GetUserBookingAsync(Guid id)
        {
            return await _bookingRepository.GetUserBookingsAsync(id);
        }

        public async Task AddAsync(BookingRequest request, Guid userId)
        {
            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null || !room.IsAvailable)
                throw new Exception("Кімната недоступна");

            var days = (request.CheckOutDate - request.CheckInDate).Days;
            var price = days * room.PricePerNight;

            var booking = new Booking
            {
                RoomId = request.RoomId,
                UserId = userId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Price = price
            };

            await _bookingRepository.CreateBookingAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            await _bookingRepository.DeleteAsync(id);
        }
    }
}
