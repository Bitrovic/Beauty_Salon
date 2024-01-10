using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IReservationRepository
    {
        public List<Reservation> GetReservations();
        public List<Reservation> GetUserReservations(string userId);
        public Reservation GetReservation(int reservationId);
        public bool CreateResetvation(Reservation reservation);
        public double GetReservationPrice(int reservationId);
        public bool DeleteReservation(int reservationId);
        public bool EditReservation(Reservation reservation);
        public bool RealiseReservation(int reservationId);
    }
}
