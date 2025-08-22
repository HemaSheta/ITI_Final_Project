using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        // ✅ Add new Grade and Save
        public async Task AddGradeAsync(Grade grade)
        {
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();  // <- required
        }

        // ✅ Load all Grades with Trainee + Session + Course
        public async Task<IEnumerable<Grade>> GetAllWithTraineeAndCourseAsync()
        {
            return await _context.Grades
                .Include(g => g.User)
                .Include(g => g.Session)
                    .ThenInclude(s => s.Course)
                .ToListAsync();
        }

        // ✅ Get Grade by Id with full navigation properties
        public async Task<Grade?> GetById(int id)
        {
            return await _context.Grades
                .Include(g => g.User)
                .Include(g => g.Session)
                    .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(g => g.GradeId == id);
        }
    }
}
