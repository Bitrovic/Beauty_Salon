using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Beauty_Salon.Repository
{
    public class BillRepository : IBillRepository
    {
        readonly ApplicationDbContext db = new();
        private readonly BillItemRepository billItemRepository = new();
        private readonly ReservationRepository reservationRepository = new();

        public List<Bill> GetUserPaymentHistory(string userId)
        {
            List<Bill> bills = db.Bills
                .Include(x => x.User)
                .Where(r => r.UserId == userId).ToList();
            return bills;
        }

        public bool CreateBill(Bill bill)
        {
            try
            {
                if (bill != null)
                {
                    db.Bills.Add(bill);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Bill - Create: " + ex.Message);
                Debug.WriteLine($" Bill - Create - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public Bill GetLastCreatedBill()
        {
            Bill? lastBill = db.Bills.OrderByDescending(b => b.Id).FirstOrDefault();
            return lastBill;
        }

        public Bill GetBill(int billId)
        {
            Bill? bill = db.Bills.Include(x => x.User).Where(r => r.Id == billId).FirstOrDefault();
            return bill;
        }

        public bool RequestReversal(int billId)
        {
            Bill? bill = db.Bills.FirstOrDefault(x => x.Id == billId);

            try
            {
                if (bill != null)
                {
                    bill.Status = "Storniranje";
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Bill - RequestReversal: " + ex.Message);
                Debug.WriteLine($" Bill - RequestReversal - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public bool Reversal(int billId)
        {
            Bill? bill = db.Bills.FirstOrDefault(x => x.Id == billId);

            try
            {
                if (bill != null)
                {
                    bill.Status = "Stornirano";

                    List<BillItem> billitems = billItemRepository.GetBillItems(billId);
                    foreach(BillItem item in billitems)
                    {
                        reservationRepository.DeleteReservation(item.ReservationId);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Bill - Reversal: " + ex.Message);
                Debug.WriteLine($" Bill - Reversal - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public List<Bill> GetBills()
        {
            List<Bill> bills = db.Bills.Include(x => x.User).ToList();
            return bills;
        }
    }
}
