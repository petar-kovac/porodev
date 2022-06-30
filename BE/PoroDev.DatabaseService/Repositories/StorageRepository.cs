using PoroDev.Common.Contracts.StorageService;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class StorageRepository : GenericRepository<FileData, SqlDataContext>, IStorageRepository
    {
        public StorageRepository(SqlDataContext context) : base(context)
        {

        }
    }
}
