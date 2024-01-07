using Beauty_Salon.Models;
using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Beauty_Salon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly TreatmentRepository treatmentRepository = new();
        private readonly ReservationRepository reservationRepository = new();
        private readonly ReservationTermRepository reservationTermRepository = new();

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Index()
        {
            List<Reservation> reservations = reservationRepository.GetReservations();
            bool payment = false;
            if(reservations != null)
            {
                foreach (Reservation reservation in reservations)
                {
                    if(reservation.Status == "Nerealizovano")
                    {
                        payment = true;
                    }
                }
                ViewBag.Payment = payment;
            }
            return View(reservations);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllReservations()
        {
            return View(reservationRepository.GetReservations());
        }

        [Authorize(Roles = "Admin, Customer")]
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

        [Authorize(Roles = "Admin, Customer")]
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

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (reservationRepository.DeleteReservation(id))
            {
                TempData["success"] = "Rezervacija uspešno izbrisana";
            }
            else
            {
                TempData["error"] = "Rezervacija neuspešno izbrisana";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (reservationRepository.DeleteReservation(id))
            {
                TempData["success"] = "Rezervacija uspešno izbrisana";
            }
            else
            {
                TempData["error"] = "Rezervacija neuspešno izbrisana";
            }

            return RedirectToAction("AllReservations");
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Reservation reservation = reservationRepository.GetReservation(id);
            if (reservation != null)
            {
                ViewBag.Treatments = treatmentRepository.GetTreatments();
                ViewBag.ReservationTerms = reservationTermRepository.GetAllReservationTerms();
                return View(reservation);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public IActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (reservationRepository.EditReservation(reservation))
                {
                    TempData["success"] = "Rezevacija uspešno ažurirana";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Rezevacija neuspešno ažurirana";
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Realise(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (reservationRepository.RealiseReservation(id))
            {
                TempData["success"] = "Rezervacija uspešno realizovana";
            }
            else
            {
                TempData["error"] = "Rezervacija neuspešno realizovana";
            }

            return RedirectToAction("AllReservations");
        }
    }
}
