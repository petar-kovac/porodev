namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRuntimeDataRepository RuntimeData { get; }

        Task<int> SaveChanges();
    }
}