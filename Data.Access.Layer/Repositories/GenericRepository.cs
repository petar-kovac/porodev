using Data.Access.Layer.Data;
using Data.Access.Layer.Models.Contracts;
using Data.Access.Layer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Access.Layer.Repositories
{
    public class GenericRepository<TemplateEntity,TemplateDatabaseContext> : IGenericRepository<TemplateEntity> where TemplateEntity : class, IUser where TemplateDatabaseContext : DbContext
    {

        private readonly TemplateDatabaseContext _context;

        public GenericRepository(TemplateDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<TemplateEntity> Query()
        {
            return _context.Set<TemplateEntity>().AsQueryable();
        }

        public async Task<TemplateEntity?> CreateAsync(TemplateEntity entity)
        {
            if (await _context.Set<TemplateEntity>().AddAsync(entity) != null)
                return entity;
            return null;
        }

        public void Delete(TemplateEntity entity)
        {
            _context.Set<TemplateEntity>().Remove(entity);
        }

        public async Task<ICollection<TemplateEntity>> GetAllAsync()
        {
            return await _context.Set<TemplateEntity>().ToListAsync();
        }

        public async Task<TemplateEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TemplateEntity>().FindAsync(id);
        }

        public async Task<TemplateEntity?> FindSingleAsync(Expression<Func<TemplateEntity, bool>> filter )
        {
            return await _context.Set<TemplateEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter)
        {
            return await _context.Set<TemplateEntity>().Where(filter).ToListAsync();
        }

        public async Task<TemplateEntity?> UpdateAsync(TemplateEntity entity, Guid id)
        {
            if (entity == null)
            {
                return null;
            }
            TemplateEntity? exist = await _context.Set<TemplateEntity>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

    }
}
