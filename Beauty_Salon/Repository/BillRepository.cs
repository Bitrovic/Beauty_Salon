using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;
using Microsoft.EntityFrameworkCore;

namespace Beauty_Salon.Repository
{
    public class BillRepository : IBillRepository
    {
        readonly ApplicationDbContext db = new();

        public List<Bill> GetUserPaymentHistory(string userId)
        {
            List<Bill> bills = db.Bills
                .Include(x => x.User)
                .Where(r => r.UserId == userId).ToList();
            return bills;
        }
    }
}
