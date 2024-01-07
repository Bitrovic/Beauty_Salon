using Beauty_Salon.Controllers;
using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IBillItemsRepository
    {
        public bool CreateBillItems(BillItem billItems);
        public List<BillItem> GetBillItems(int billId);
    }
}
