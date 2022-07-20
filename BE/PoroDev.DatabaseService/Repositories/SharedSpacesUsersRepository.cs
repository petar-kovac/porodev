using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class SharedSpacesUsersRepository : GenericRepository<SharedSpacesUsers, SqlDataContext>, ISharedSpacesUsersRepository
    {
        public SharedSpacesUsersRepository(SqlDataContext context) : base(context)
        {
        }
    }
}
