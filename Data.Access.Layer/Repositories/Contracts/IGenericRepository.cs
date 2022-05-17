using System.Linq.Expressions;

namespace Data.Access.Layer.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<IEnumerable<T?>> GetAll();

        Task<T?> GetById(Guid Id);

        Task<T?> Create(T entity);

        Task<int?> Delete(T entity);

        Task<T?> Update(T entity, Guid id);

        Task<T?> Find(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T?>> FindAll(Expression<Func<T, bool>> filter);
    }
}
