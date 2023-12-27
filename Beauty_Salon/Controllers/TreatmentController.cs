using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Salon.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly TreatmentRepository treatmentRepository = new();
        
        public IActionResult Index()
        {
            return View(treatmentRepository.GetTreatments());
        }
    }
}
