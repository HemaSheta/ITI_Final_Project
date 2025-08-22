using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Repositories.Implementations
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Grade>> GetAllWithTraneeAndCourseAsync()
        {
            var gradeView = await _context.Grades.Include(g => g.User)
                           .Include(g => g.Session)
                                .ThenInclude(g => g.Course)
                            .ToListAsync();
            return (IEnumerable<Grade>)gradeView;
        }


    }
}
