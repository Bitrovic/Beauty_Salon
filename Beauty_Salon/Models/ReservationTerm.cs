using System;
using System.Collections.Generic;

namespace Beauty_Salon.Models
{
    public partial class ReservationTerm
    {
        public ReservationTerm()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
