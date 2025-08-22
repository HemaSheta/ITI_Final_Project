using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private new readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllInstructorsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Instructor")   
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }
    }
}
