using Microsoft.EntityFrameworkCore;
using PoroDev.Database.Repositories.Contracts;
using System.Linq.Expressions;

namespace PoroDev.Database.Repositories
{
    public class GenericRepository<TemplateEntity, TemplateDatabaseContext> :
        IGenericRepository<TemplateEntity> where TemplateEntity : class, new()
        where TemplateDatabaseContext : DbContext
    {
        private readonly TemplateDatabaseContext _context;

        public GenericRepository(TemplateDatabaseContext context)
        {
            _context = context;
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

        public async Task<TemplateEntity?> FindSingleAsync(Expression<Func<TemplateEntity, bool>> filter)
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