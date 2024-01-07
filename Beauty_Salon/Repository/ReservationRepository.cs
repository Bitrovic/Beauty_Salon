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
        private readonly TreatmentRepository treatmentRepository = new();

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

        public bool DeleteReservation(int reservationId)
        {
            Reservation? reservation = db.Reservations.FirstOrDefault(x => x.Id == reservationId);

            try
            {
                if (reservation != null)
                {
                    reservation.Status = "Otkazano";
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Reservation - Delete: " + ex.Message);
                Debug.WriteLine($" Reservation - Delete - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public bool EditReservation(Reservation reservation)
        {
            Reservation? reservation1 = db.Reservations.FirstOrDefault(x => x.Id == reservation.Id);

            try
            {
                if (reservation1 != null)
                {
                    reservation1.Id = reservation.Id;
                    reservation1.UserId = reservation.UserId;
                    reservation1.TreatmentId = reservation.TreatmentId;
                    reservation1.ReservationTermId = reservation.ReservationTermId;
                    reservation1.ReservationDate = reservation.ReservationDate;
                    reservation1.Status = reservation.Status;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Treatment - Edit: " + ex.Message);
                Debug.WriteLine($" Treatment - Edit - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public Reservation GetReservation(int reservationId)
        {
            Reservation? reservation = db.Reservations.Include(x => x.Treatment).FirstOrDefault(r => r.Id == reservationId);
            return reservation;
        }


        public double GetReservationPrice(int reservationId)
        {
            Reservation reservation = GetReservation(reservationId);
            double price = 0;
            
            if(reservation != null)
            {
                Treatment treatment = treatmentRepository.GetTreatmentById(reservation.TreatmentId);
                if (treatment != null)
                {
                    price = (double)treatment.Price;
                }
            }
            return price;
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = db.Reservations.Include(x => x.ReservationTerm).Include(x => x.User).Include(x => x.Treatment).ToList();
            return reservations;
        }

        public bool RealiseReservation(int reservationId)
        {
            Reservation? reservation = db.Reservations.FirstOrDefault(x => x.Id == reservationId);

            try
            {
                if (reservation != null)
                {
                    reservation.Status = "Realizovano";
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Reservation - RealiseReservation: " + ex.Message);
                Debug.WriteLine($" Reservation - RealiseReservation - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }
    }
}
