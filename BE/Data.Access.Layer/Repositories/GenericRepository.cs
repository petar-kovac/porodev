using Data.Access.Layer.Data;
using Data.Access.Layer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Access.Layer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {

        private readonly SqlDataContext _context;

        public GenericRepository(SqlDataContext context)
        {
            _context = context;
        }

        public async Task<T?> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int?> Delete(T entity)
        {
            var deletedEntry = _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T?>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<T?> Find(Expression<Func<T, bool>> filter )
        {
            return _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T?> Update(T entity, Guid id)
        {
            if (entity == null)
                return null;
            T? exist = await _context.Set<T>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<IEnumerable<T?>> FindAll(Expression<Func<T, bool>> filter )
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }
    }
}
