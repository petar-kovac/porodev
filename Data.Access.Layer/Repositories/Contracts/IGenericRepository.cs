using Data.Access.Layer.Models.Contracts;
using System.Linq.Expressions;

namespace Data.Access.Layer.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class, IUser
    {
        IQueryable<TEntity> Query();

        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid Id);

        Task<TEntity?> CreateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<TEntity?> UpdateAsync(TEntity entity, Guid id);

        Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> filter);

        Task<ICollection<TEntity>?> FindAllAsync(Expression<Func<TEntity, bool>> filter);
    }
}
