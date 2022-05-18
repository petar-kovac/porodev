using Data.Access.Layer.Repositories.Contracts;
using Data.Access.Layer.Data;

namespace Data.Access.Layer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlDataContext _context;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(SqlDataContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

    }
}
