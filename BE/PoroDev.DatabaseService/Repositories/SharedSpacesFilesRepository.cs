using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class SharedSpacesFilesRepository : GenericRepository<SharedSpacesFiles, SqlDataContext>, ISharedSpacesWithFilesRepository
    {
        public SharedSpacesFilesRepository(SqlDataContext context) : base(context)
        {

        }

        public async Task<List<SharedSpacesFiles>> QueryFilesBySpaceId(Guid spaceId)
        {
            var returnList = await _context.SharedSpacesFiles
                           .Include(x => x.File)
                           .Where(x => x.SharedSpaceId.Equals(spaceId))
                           .ToListAsync();

            return returnList;
        }
    }
}
