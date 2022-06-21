namespace PoroDev.Database.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        Task<int> SaveChanges();
    }
}