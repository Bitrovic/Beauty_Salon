using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface ITreatmentRepository
    {
        public List<Treatment> GetTreatments();
    }
}
