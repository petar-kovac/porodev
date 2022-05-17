using Data.Access.Layer.Repositories.Contracts;
using Data.Access.Layer.Data;

namespace Data.Access.Layer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlDataContext _context;

        public UnitOfWork(SqlDataContext context)
        {
            _context = context;
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
