namespace Hotel.API.Requests
{
    public class UpdatePriceRequest
    {
        public string Type { get; set; }
        public decimal NewPrice { get; set; }
    }
}
