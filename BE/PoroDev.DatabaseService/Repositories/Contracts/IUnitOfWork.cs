using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.Database.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IStorageRepository UserFiles { get; }

        Task<int> SaveChanges();
    }
}