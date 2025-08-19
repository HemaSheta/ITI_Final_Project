using Project_Final_ITI.Models;

namespace Training_Managment_System.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        // Extra User-specific queries (if needed)
        Task<User?> GetUserByEmail(string email);
    }
}
