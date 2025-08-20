using Project_Final_ITI.Models;

namespace Training_Managment_System.Repositories.Interfaces
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllWithCourseAsync();
        Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName);
    }
}
