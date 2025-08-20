using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class GradeRepository :  BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
