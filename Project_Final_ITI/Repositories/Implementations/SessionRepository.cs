using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        private readonly ApplicationDbContext cont;

        public SessionRepository(ApplicationDbContext context) : base(context)
        {
            cont = context;
        }

        public async Task<IEnumerable<Session>> GetAllWithCourseAsync()
        {
            return await cont.Sessions
                .Include(s => s.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName)
        {
            return await cont.Sessions
                                 .Include(s => s.Course)
                                 .Where(s => s.Course.CourseName.Contains(courseName))
                                 .ToListAsync();
        }
    }
}
