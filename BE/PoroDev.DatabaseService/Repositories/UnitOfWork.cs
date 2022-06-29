using PoroDev.Database.Data;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDataContext _context;
        private bool _disposed;

        public IUserRepository Users { get; }
        public IStorageRepository UserFiles { get; }

        public UnitOfWork(SqlDataContext context, IUserRepository users, IStorageRepository userFiles)
        {
            _context = context;
            Users = users;
            UserFiles = userFiles;
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