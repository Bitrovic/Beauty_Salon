using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;
using System.Diagnostics;

namespace Beauty_Salon.Repository
{
    public class TreatmentRepository : ITreatmentRepository
    {
        readonly ApplicationDbContext db = new();

        public bool CreateTreatment(Treatment treatment)
        {
            try
            {
                if (treatment != null)
                {
                    treatment.Image = "images/treatments/" + treatment.Image;
                    db.Treatments.Add(treatment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Treatment - Create: " + ex.Message);
                Debug.WriteLine($" Treatment - Create - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public bool DeleteTreatment(int id)
        {
            Treatment? treatment = db.Treatments.FirstOrDefault(x => x.Id == id);

            try
            {
                if (treatment != null)
                {
                    db.Treatments.Remove(treatment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Treatment - Delete: " + ex.Message);
                Debug.WriteLine($" Treatment - Delete - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public bool EditTreatment(Treatment treatment)
        {
            Treatment? treatment1 = db.Treatments.FirstOrDefault(x => x.Id == treatment.Id);

            try
            {
                if (treatment1 != null)
                {
                    treatment1.Id = treatment.Id;
                    treatment1.Name = treatment.Name;
                    treatment1.Description = treatment.Description;
                    treatment1.Price = treatment.Price;
                    treatment1.Image = "images/treatments/" + treatment.Image;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Treatment - Edit: " + ex.Message);
                Debug.WriteLine($" Treatment - Edit - Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public Treatment GetTreatmentById(int id)
        {
            Treatment? treatment = db.Treatments.FirstOrDefault(x =>x.Id == id);
            return treatment!;
        }

        public List<Treatment> GetTreatments()
        {
            List<Treatment> treatments = db.Treatments.ToList();
            return treatments;
        }
    }
}
