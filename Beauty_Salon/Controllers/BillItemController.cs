using Beauty_Salon.Models;
using Beauty_Salon.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Salon.Controllers
{
    public class BillItemController : Controller
    {
        private readonly BillItemRepository billItemRepository = new();
        private readonly BillRepository billRepository = new();

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            List<BillItem> billItems = billItemRepository.GetBillItems(id);
            if (billItems != null)
            {
                ViewBag.Bill = billRepository.GetBill(billItems[0].BillId);
                return View(billItems);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
