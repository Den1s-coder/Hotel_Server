﻿using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
using Hotel.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastracture.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDBContext _context;

        public BookingRepository(HotelDBContext context)
        {
            _context = context;
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            bool available = await IsRoomAvailable(booking);

            if (!available)
            {
                throw new InvalidOperationException("Кімната вже заброньована на вибраний період.");
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (booking != null) 
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        private async Task<bool> IsRoomAvailable(Booking request)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.RoomId == request.RoomId &&
                (
                    (request.CheckInDate < b.CheckOutDate && request.CheckOutDate > b.CheckInDate)
                )
            );
        }
    }
}
