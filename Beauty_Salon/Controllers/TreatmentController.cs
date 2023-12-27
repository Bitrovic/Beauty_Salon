using Microsoft.AspNetCore.Mvc;

namespace Beauty_Salon.Controllers
{
    public class TreatmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
