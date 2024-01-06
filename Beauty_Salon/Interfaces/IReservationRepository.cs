using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IReservationRepository
    {
        public List<Reservation> GetReservations();
        public bool CreateResetvation(Reservation reservation);
    }
}
