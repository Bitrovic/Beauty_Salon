using System;
using System.Collections.Generic;

namespace Beauty_Salon.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
