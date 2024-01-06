using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IReservationTermRepository
    {
        public List<ReservationTerm> GetAllReservationTerms();
    }
}
