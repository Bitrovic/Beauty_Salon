using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;

namespace Beauty_Salon.Repository
{
    public class ReservationTermRepository : IReservationTermRepository
    {
        readonly ApplicationDbContext db = new();

        public List<ReservationTerm> GetAllReservationTerms()
        {
            List<ReservationTerm> reservationTerms = db.ReservationTerms.ToList();
            return reservationTerms;
        }
    }
}
