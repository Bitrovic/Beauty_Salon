using Beauty_Salon.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                if (treatmentRepository.CreateTreatment(treatment))
                {
                    TempData["success"] = "Tretman uspešno kreiran";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Tretman neuspešno kreiran";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Treatment treatment = treatmentRepository.GetTreatmentById(id);
            if (treatment != null)
            {
                return View(treatment);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Treatment treatment)
        {
            if(ModelState.IsValid)
            {
                if(treatmentRepository.EditTreatment(treatment))
                {
                    TempData["success"] = "Tretman uspešno ažuriran";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Tretman neuspešno ažuriran";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Treatment treatment = treatmentRepository.GetTreatmentById(id);
            if (treatment != null)
            {
                return View(treatment);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Delete(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                if (treatmentRepository.DeleteTreatment(treatment.Id))
                {
                    TempData["success"] = "Tretman uspešno obrisan";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Tretman neuspešno obrisan";
                    return View();
                }
            }
            return View();
        }
    }
}
