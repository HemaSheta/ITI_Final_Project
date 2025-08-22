using Project_Final_ITI.Models;

namespace Training_Managment_System.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {

        //Task<Course> Search(String? name ,string? category) { }
        Task<IEnumerable<Course>> GetAllWithInstructorAsync();
        Task<IEnumerable<Course>> FindWithInstructor(System.Linq.Expressions.Expression<Func<Course, bool>> predicate);
        Task<Course?> GetCourseWithInstructor(int id);


    }
}
