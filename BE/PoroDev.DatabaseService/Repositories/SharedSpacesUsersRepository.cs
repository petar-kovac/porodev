using Microsoft.EntityFrameworkCore;
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


        public async Task<List<SharedSpacesUsers>> GetSharedSpacesByUserId(Guid userId)
        {
            var returnList = await _context.SharedSpacesUsers
                .Include(sharedSpaceUsers => sharedSpaceUsers.SharedSpace)
                .Where(sharedSpaceUsers => sharedSpaceUsers.UserId.Equals(userId))
                .ToListAsync();
            return returnList;
        }
    }
}
