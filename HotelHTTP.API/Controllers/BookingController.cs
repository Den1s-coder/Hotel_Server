using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
