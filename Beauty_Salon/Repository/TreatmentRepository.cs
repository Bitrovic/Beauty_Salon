using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;

namespace Beauty_Salon.Repository
{
    public class TreatmentRepository : ITreatmentRepository
    {
        readonly ApplicationDbContext db = new();

        public List<Treatment> GetTreatments()
        {
            List<Treatment> treatments = db.Treatments.ToList();
            return treatments;
        }
    }
}
