using Data.Access.Layer.Data;
using Data.Access.Layer.Models.Contracts;
using Data.Access.Layer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Access.Layer.Repositories
{
    public class GenericRepository<TEntity,TDbContext> : IGenericRepository<TEntity> where TEntity : class, IUser where TDbContext : DbContext
    {

        private readonly TDbContext _context;

        public GenericRepository(TDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity?> CreateAsync(TEntity entity)
        {
            if (await _context.Set<TEntity>().AddAsync(entity) != null)
                return entity;
            return null;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> filter )
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<ICollection<TEntity>?> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity?> UpdateAsync(TEntity entity, Guid id)
        {
            if (entity == null)
            {
                return null;
            }
            TEntity? exist = await _context.Set<TEntity>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

    }
}
