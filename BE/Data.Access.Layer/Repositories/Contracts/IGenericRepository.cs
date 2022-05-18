using Data.Access.Layer.Models.Contracts;
using System.Linq.Expressions;

namespace Data.Access.Layer.Repositories.Contracts
{
    public interface IGenericRepository<TemplateEntity> where TemplateEntity : class, IUser
    {
        IQueryable<TemplateEntity> Query();

        Task<ICollection<TemplateEntity>> GetAllAsync();

        Task<TemplateEntity?> GetByIdAsync(Guid Id);

        Task<TemplateEntity?> CreateAsync(TemplateEntity entity);

        void Delete(TemplateEntity entity);

        Task<TemplateEntity?> UpdateAsync(TemplateEntity entity, Guid id);

        Task<TemplateEntity?> FindSingleAsync(Expression<Func<TemplateEntity, bool>> filter);

        Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter);
    }
}
