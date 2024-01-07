using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface ITreatmentRepository
    {
        public List<Treatment> GetTreatments();
        public Treatment GetTreatmentById(int? id);
        public bool CreateTreatment(Treatment treatment);
        public bool EditTreatment(Treatment treatment);
        public bool DeleteTreatment(int id);
    }
}
