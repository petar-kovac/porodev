using PoroDev.Common.Models.StorageModels.Data;
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