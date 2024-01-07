using Beauty_Salon.Models;
using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Beauty_Salon.Controllers
{
    public class BillController : Controller
    {
        private readonly TreatmentRepository treatmentRepository = new();
        private readonly ReservationRepository reservationRepository = new();
        private readonly ReservationTermRepository reservationTermRepository = new();
        private readonly BillRepository billRepository = new();

        public IActionResult Index()
        {
            var loggedUserJson = HttpContext.Session.GetString("LoggedUser");

            if (!string.IsNullOrEmpty(loggedUserJson))
            {
                var user = JsonConvert.DeserializeObject<User>(loggedUserJson);
                if (user != null && !string.IsNullOrEmpty(user.Id))
                {
                    return View(billRepository.GetUserPaymentHistory(user.Id));
                }
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<int> selectedReservations)
        {
            Debug.WriteLine("List of selected Reservations: \n");
            foreach(var r in selectedReservations)
            {
                Debug.WriteLine(r);
            }
            return View("Index", "Home");
        }
    }
}
