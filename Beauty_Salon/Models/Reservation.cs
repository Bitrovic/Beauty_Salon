using System;
using System.Collections.Generic;

namespace Beauty_Salon.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TreatmentId { get; set; }
        public int? ReservationTermId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public string? Status { get; set; }

        public virtual ReservationTerm? ReservationTerm { get; set; }
        public virtual Treatment? Treatment { get; set; }
    }
}
