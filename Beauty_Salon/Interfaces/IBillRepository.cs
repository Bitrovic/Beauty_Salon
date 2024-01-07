using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IBillRepository
    {
        public List<Bill> GetUserPaymentHistory(string userId);
    }
}
