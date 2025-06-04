using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public Guid UserId {  get; set; }
        public User User { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Price { get; set; }

    }
}
