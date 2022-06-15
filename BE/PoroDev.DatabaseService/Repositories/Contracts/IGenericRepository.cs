using PoroDev.Common.Models.UnitOfWorkResponse;
using System.Linq.Expressions;

namespace PoroDev.Database.Repositories.Contracts
{
    public interface IGenericRepository<TemplateEntity> where TemplateEntity : class, new()
    {
        Task<ICollection<TemplateEntity>> GetAllAsync();

        Task<TemplateEntity?> GetByIdAsync(Guid Id);

        Task<UnitOfWorkResponseModel<TemplateEntity>> CreateAsync(TemplateEntity entity);

        Task<UnitOfWorkResponseModel<TemplateEntity>> Delete(TemplateEntity entity);

        Task<UnitOfWorkResponseModel<TemplateEntity>> UpdateAsync(TemplateEntity entity, Guid id);

        Task<UnitOfWorkResponseModel<TemplateEntity>> FindAsync(Expression<Func<TemplateEntity, bool>> filter);

        Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter);
    }
}