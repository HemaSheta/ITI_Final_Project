using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext cont;

        public ICourseRepository CourseRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                          ICourseRepository courseRepository,
                          IUserRepository userRepository,
                          ISessionRepository sessionRepository)
        {
            cont = context;
            CourseRepository = courseRepository;
            SessionRepository = sessionRepository;
            UserRepository = userRepository;
        }

        public Task<int> SaveAsync() => cont.SaveChangesAsync();
        public async Task<int> CompleteAsync()
        {
            return await cont.SaveChangesAsync();
        }
    }
}
