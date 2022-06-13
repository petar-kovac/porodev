using System.Linq.Expressions;

namespace PoroDev.Database.Repositories.Contracts
{
    public interface IGenericRepository<TemplateEntity> where TemplateEntity : class, new()
    {
        Task<ICollection<TemplateEntity>> GetAllAsync();

        Task<TemplateEntity?> GetByIdAsync(Guid Id);

        Task<TemplateEntity?> CreateAsync(TemplateEntity entity);

        void Delete(TemplateEntity entity);

        Task<TemplateEntity?> UpdateAsync(TemplateEntity entity, Guid id);

        Task<TemplateEntity?> FindAsync(Expression<Func<TemplateEntity, bool>> filter);

        Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter);
    }
}