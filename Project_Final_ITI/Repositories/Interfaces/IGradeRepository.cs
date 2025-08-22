using Project_Final_ITI.Models;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Repositories.Interfaces
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetAllWithTraneeAndCourseAsync();

    }

}
