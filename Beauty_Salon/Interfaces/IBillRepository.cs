using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IBillRepository
    {
        public List<Bill> GetUserPaymentHistory(string userId);
        public bool CreateBill(Bill bill);
        public Bill GetLastCreatedBill();
        public Bill GetBill(int billId);
        public bool RequestReversal(int billId);
        public bool Reversal(int billId);
        public List<Bill> GetBills();
    }
}
