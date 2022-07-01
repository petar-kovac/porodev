namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRuntimeDataRepository RuntimeData { get; }

        IStorageRepository UserFiles { get; }

        Task<int> SaveChanges();
    }
}