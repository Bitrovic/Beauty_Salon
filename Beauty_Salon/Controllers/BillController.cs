using Beauty_Salon.Models;
using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Authorization;
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
        private readonly BillItemRepository billItemRepository = new();

        [Authorize(Roles = "Admin, Customer")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult AllBills()
        {
            return View(billRepository.GetBills());
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Create()
        {
            return View("Index");
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public IActionResult Create(List<int> selectedReservations)
        {
            if (ModelState.IsValid)
            {
                if(selectedReservations != null)
                {
                    var loggedUserJson = HttpContext.Session.GetString("LoggedUser");

                    if (!string.IsNullOrEmpty(loggedUserJson))
                    {
                        var user = JsonConvert.DeserializeObject<User>(loggedUserJson);
                        if (user != null && !string.IsNullOrEmpty(user.Id))
                        {
                            Bill bill = new();
                            bill.TotalPrice = GetTotalPrice(selectedReservations);
                            bill.CreationDate = DateTime.Now;
                            bill.UserId = user.Id;
                            bill.Status = "Izdato";
                            bool error = false;

                            if (billRepository.CreateBill(bill))
                            {
                                Bill lastBill = billRepository.GetLastCreatedBill();
                                if (lastBill != null)
                                {
                                    foreach (var selectedReservation in selectedReservations)
                                    {
                                        Reservation reservation = reservationRepository.GetReservation(selectedReservation);

                                        BillItem billItem = new();
                                        billItem.BillId = lastBill.Id;
                                        billItem.ReservationId = reservation.Id;
                                        billItem.Price = (float)reservation.Treatment.Price;

                                        if (!billItemRepository.CreateBillItems(billItem))
                                        {
                                           error = true;
                                        }
                                    }
                                    if (error)
                                    {
                                        TempData["error"] = "Račun neuspešno kreiran";
                                    }
                                    else
                                    {
                                        TempData["success"] = "Račun uspešno kreiran";
                                    }
                                    return RedirectToAction("Index", "Reservation");
                                }
                            }
                            TempData["error"] = "Račun neuspešno kreiran";
                            return RedirectToAction("Index", "Reservation");
                        }
                    }
                    TempData["error"] = "Sesija nije validna";
                    return RedirectToAction("Index", "Reservation");
                }
                else
                {
                    TempData["error"] = "Račun neuspešno kreiran";
                    return RedirectToAction("Index", "Reservation");
                }
            }
            TempData["error"] = "Račun neuspešno kreiran";
            return RedirectToAction("Index", "Reservation");
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult RequestReversal(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (billRepository.RequestReversal(id))
            {
                TempData["success"] = "Zahtev za storniranje uspešno podnet";
            }
            else
            {
                TempData["error"] = "Zahtev za storniranje neuspešno podnet";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Reversal(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (billRepository.Reversal(id))
            {
                TempData["success"] = "Račun uspešno storniran";
            }
            else
            {
                TempData["error"] = "Račun neuspešno storniran";
            }

            return RedirectToAction("Index");
        }

        private float GetTotalPrice(List<int> selectedReservations)
        {
            float totalPrice = 0;

            foreach (var reservation in selectedReservations)
            {
                totalPrice += (float)reservationRepository.GetReservationPrice(reservation);
            }
            return totalPrice;
        }
    }
}
