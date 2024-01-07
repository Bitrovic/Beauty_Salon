namespace Beauty_Salon.Models
{
    public class BillItem
    {
        public int Id { get; set; }
        public int BillId { get; set;}
        public int ReservationId { get; set; }
        public float Price { get; set; }

        public virtual Bill? Bill { get; set; }
        public virtual Reservation? Reservation { get; set; }
    }
}
