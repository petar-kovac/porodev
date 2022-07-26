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
                .Include(usersSharedSpaces => usersSharedSpaces.SharedSpace)
                .Where(sharedSpacesUsers => sharedSpacesUsers.UserId.Equals(userId))
                .ToListAsync();
            return returnList;
        }

        public async Task<List<SharedSpacesUsers>> GetAllUsersBySharedSpaceId(Guid sharedSpaceId)
        {
            var returnList = await _context.SharedSpacesUsers
                .Include(sharedSpacesUsers => sharedSpacesUsers.User)
                .Where(sharedSpacesUsers => sharedSpacesUsers.SharedSpaceId.Equals(sharedSpaceId))
                .ToListAsync();
            return returnList;
        }
    }
}