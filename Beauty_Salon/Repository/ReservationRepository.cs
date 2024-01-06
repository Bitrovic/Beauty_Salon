using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Beauty_Salon.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        readonly ApplicationDbContext db = new();

        public bool CreateResetvation(Reservation reservation)
        {
            try
            {
                if (reservation != null)
                {
                    reservation.Status = "Nerealizovano";
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Reservation - Create: " + ex.Message);
                Debug.WriteLine($" Reservation - Create - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = db.Reservations.Include(x => x.ReservationTerm).Include(x => x.User).ToList();
            return reservations;
        }
    }
}
