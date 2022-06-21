using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.Database.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRuntimeDataRepository RuntimeData {get;}

        Task<int> SaveChanges();
    }
}