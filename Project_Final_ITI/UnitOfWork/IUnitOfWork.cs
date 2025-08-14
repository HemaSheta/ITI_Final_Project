using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }

    }
}
