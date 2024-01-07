using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Address { get; set; }

        public override string? Email { get; set; }

        public override string? PhoneNumber { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
