using PoroDev.Common.Contracts.StorageService;
using PoroDev.Database.Data;
using PoroDev.Database.Repositories;
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
