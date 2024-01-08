using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv tretmana")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Naziv tretmana mora imati između 3 i 50 karaktera")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Morate uneti opis tretmana")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Opis tretmana mora imati između 5 i 250 karaktera")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Morate uneti cenu tretmana")]
        [Range(1, 9999999, ErrorMessage = "Cena tretmana mora biti veća od 0 a manja od 9999999 dinara")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Morate uneti sliku tretmana")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Slika tretmana mora imati bar 5 karaktera")]
        public string? Image { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
