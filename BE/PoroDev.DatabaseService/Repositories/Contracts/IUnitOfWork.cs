namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRuntimeDataRepository RuntimeData { get; }

        IStorageRepository UserFiles { get; }

        ISharedSpaceRepository SharedSpaces { get; }

        ISharedSpacesWithFilesRepository SharedSpacesWithFiles { get; }

        ISharedSpacesUsersRepository SharedSpacesUsers { get; }

        IUserReportsRepository UserReports { get; }

        Task<int> SaveChanges();
    }
}