using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDataContext _context;
        private bool _disposed;

        public IUserRepository Users { get; }
        public IStorageRepository UserFiles { get; }

        public IRuntimeDataRepository RuntimeData { get; }

        public IUserReportsRepository UserReports { get; }

        //public UnitOfWork(SqlDataContext context, IUserRepository users, IStorageRepository userFiles, IRuntimeDataRepository runtimeData, IUserReportsRepository userReports)
        public ISharedSpaceRepository SharedSpaces { get; }

        public ISharedSpacesWithFilesRepository SharedSpacesWithFiles { get; }

        public ISharedSpacesUsersRepository SharedSpacesUsers { get; }

        public UnitOfWork(SqlDataContext context,
                          IUserRepository users,
                          IStorageRepository userFiles,
                          IRuntimeDataRepository runtimeData,
                          ISharedSpaceRepository sharedSpaceRepository,
                          ISharedSpacesWithFilesRepository sharedSpacesWithFiles,
                          ISharedSpacesUsersRepository sharedSpacesUsers,
                          IUserReportsRepository userReports)
        

        {
            _context = context;
            Users = users;
            RuntimeData = runtimeData;
            UserFiles = userFiles;
            UserReports = userReports;
            SharedSpaces = sharedSpaceRepository;
            SharedSpacesUsers = sharedSpacesUsers;
            SharedSpacesWithFiles = sharedSpacesWithFiles;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}