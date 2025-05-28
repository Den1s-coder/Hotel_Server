namespace Hotel.API.Requests
{
    public class RoomRequest
    {
        public string Number { get; set; } = string.Empty;
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; } = 1;
        public IFormFile? Image { get; set; }
    }
}
