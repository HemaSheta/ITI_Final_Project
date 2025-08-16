using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using System.Linq.Expressions;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }


        public Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }


        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

    }
}
