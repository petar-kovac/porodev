using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class SharedSpaceRepository : GenericRepository<SharedSpace, SqlDataContext>, ISharedSpaceRepository
    {
        public SharedSpaceRepository(SqlDataContext context) : base(context)
        {
        }
    }
}