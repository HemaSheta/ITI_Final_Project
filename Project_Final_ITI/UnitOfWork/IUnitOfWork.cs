using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        ISessionRepository SessionRepository { get; }
        IUserRepository UserRepository { get; }

        // saving changes
        Task<int> SaveAsync();
        Task<int> CompleteAsync();

    }
}
