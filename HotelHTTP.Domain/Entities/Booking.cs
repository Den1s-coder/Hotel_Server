using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Price { get; set; }

        public void UpdateDates(DateTime newCheckIn, DateTime newCheckOut, decimal pricePerNight)
        {
            if (newCheckIn >= newCheckOut)
                throw new ArgumentException("Invalid dates");

            CheckInDate = newCheckIn;
            CheckOutDate = newCheckOut;
            Price = CalculateTotalPrice(pricePerNight);
        }

        private decimal CalculateTotalPrice(decimal pricePerNight)
        {
            int days = (CheckOutDate - CheckInDate).Days;
            return days * pricePerNight;
        }
    }
}
