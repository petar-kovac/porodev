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
    }
}
