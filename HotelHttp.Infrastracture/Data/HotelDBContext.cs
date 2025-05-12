using Microsoft.EntityFrameworkCore;
using Hotel.Domain.Entitys;

namespace Hotel.Infrastracture.Data
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) :base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users {  get; set; }
    }
}
