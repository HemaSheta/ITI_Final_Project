using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ICourseRepository course { get; }
        public ISessionRepository session { get; }
        public IUserRepository user { get; }
        public IGradeRepository grade { get; }

        Task<int> Complete();

    }
}
