using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Morate izabrati tretman")]
        public int? TreatmentId { get; set; }
        [Required(ErrorMessage = "Morate uneti termin zakazanog tretmana")]
        public int? ReservationTermId { get; set; }
        [Required(ErrorMessage = "Morate uneti datum rezervacije")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReservationDate { get; set; }
        public string? Status { get; set; }

        public virtual ReservationTerm? ReservationTerm { get; set; }
        public virtual Treatment? Treatment { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<BillItem>? BillItems { get; set; }
    }
}
