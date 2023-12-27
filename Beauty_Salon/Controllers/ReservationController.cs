using Microsoft.AspNetCore.Mvc;

namespace Beauty_Salon.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
