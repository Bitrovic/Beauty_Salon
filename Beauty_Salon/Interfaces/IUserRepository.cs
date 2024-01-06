using Beauty_Salon.Models;

namespace Beauty_Salon.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
    }
}
