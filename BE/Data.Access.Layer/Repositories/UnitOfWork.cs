susing Data.Access.Layer.Data;
using Data.Access.Layer.Repositories.Contracts;

namespace Data.Access.Layer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDataContext _context;
        private bool _disposed;

        public IUserRepository Users { get; }

        public UnitOfWork(SqlDataContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
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