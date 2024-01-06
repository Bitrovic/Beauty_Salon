using Beauty_Salon.Models;
using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Beauty_Salon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly TreatmentRepository treatmentRepository = new();
        private readonly ReservationRepository reservationRepository = new();
        private readonly ReservationTermRepository reservationTermRepository = new();

        public IActionResult Index()
        {
            return View(reservationRepository.GetReservations());
        }

        public IActionResult Create(int treatmentId)
        {
            if (treatmentId == 0)
            {
                return NotFound();
            }

            var reservation = new Reservation
            {
                Treatment = treatmentRepository.GetTreatmentById(treatmentId)
            };

            ViewBag.ReservationTerms = reservationTermRepository.GetAllReservationTerms();

            return View(reservation);
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var loggedUserJson = HttpContext.Session.GetString("LoggedUser");

                if (!string.IsNullOrEmpty(loggedUserJson))
                {
                    var user = JsonConvert.DeserializeObject<User>(loggedUserJson);
                    if (user != null && !string.IsNullOrEmpty(user.Id))
                    {
                        reservation.UserId = user.Id;

                        if (reservationRepository.CreateResetvation(reservation))
                        {
                            TempData["success"] = "Rezervacija uspešno kreirana";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["error"] = "Rezervacija neuspešno kreirana";
                            return View();
                        }
                    }    
                }
                else
                {
                    TempData["error"] = "Sesija nije validna";
                    return View();
                }
            }
            return View();
        }

    }
}
