using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Morate uneti ime")]
        [Display(Name = "Ime")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Ime mora da ima bar 2 karaktera")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Morate uneti prezime")]
        [Display(Name = "Prezime")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prezime mora da ima bar 2 karaktera")]
        public string? Surname { get; set; }
        [Required(ErrorMessage = "Morate uneti adresu")]
        [Display(Name = "Adresa")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Adresa mora da ima bar 5 karaktera")]

        public string? Address { get; set; }
        [Required(ErrorMessage = "Morate uneti E-mail")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Email je unet u pogrešnom formatu")]
        public override string? Email { get; set; }
        [Required(ErrorMessage = "Morate uneti broj telefona")]
        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Broj telefona je unet u pogrešnom formatu")]
        public override string? PhoneNumber { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
