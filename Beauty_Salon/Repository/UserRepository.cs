using Beauty_Salon.Data;
using Beauty_Salon.Interfaces;
using Beauty_Salon.Models;

namespace Beauty_Salon.Repository
{
    public class UserRepository : IUserRepository
    { 
        readonly ApplicationDbContext db = new();

        public List<User> GetUsers()
        {
            List<User> users = db.Users.ToList();
            return users;
        }
    }
}
