using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using System.Linq.Expressions;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllWithInstructorAsync()
        {
            return await _context.Courses
                .Include(s => s.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<Course>> FindWithInstructor(Expression<Func<Course, bool>> predicate)
        {
            return await _context.Courses
                .Include(c => c.User)
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<Course?> GetCourseWithInstructor(int id)
        {
            return await _context.Courses
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }



    }
}

