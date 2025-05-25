namespace Hotel.API.Requests
{
    public class RoomRequest
    {
        public string Number { get; set; } = string.Empty;
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }
        public IFormFile Image { get; set; }
    }
}
