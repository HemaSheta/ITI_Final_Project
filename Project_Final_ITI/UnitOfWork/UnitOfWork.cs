using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICourseRepository course { get; }
        public ISessionRepository session { get; }
        public IUserRepository user { get; }
        public IGradeRepository grade { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IGradeRepository grades,
            ICourseRepository courses,
            ISessionRepository sessions,
            IUserRepository users)
        {
            _context = context;
            course = courses;
            session = sessions;
            user = users;
            grade = grades;

        }


        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
