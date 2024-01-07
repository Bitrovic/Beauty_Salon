using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }
        public string? Status { get; set; }
        public double? TotalPrice { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<BillItem>? BillItems { get; set; }
    }
}
