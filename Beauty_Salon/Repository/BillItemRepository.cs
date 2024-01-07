using Beauty_Salon.Controllers;
using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Beauty_Salon.Repository
{
    public class BillItemRepository : IBillItemsRepository
    {
        readonly ApplicationDbContext db = new();

        public bool CreateBillItems(BillItem billItem)
        {
            try
            {
                if (billItem != null)
                {
                    db.BillItem.Add(billItem);
                    Reservation? reservation = db.Reservations.FirstOrDefault(x => x.Id == billItem.ReservationId);
                    reservation.Status = "Plaćeno";
                    db.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BillItems - Create: " + ex.Message);
                Debug.WriteLine($" BillItems - Create - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public List<BillItem> GetBillItems(int billId)
        {
            List<BillItem> billItems = db.BillItem
                .Include(x => x.Reservation)
                .ThenInclude(y => y.Treatment)
                .Where(r => r.BillId == billId).ToList();

            return billItems;
        }
    }
}
