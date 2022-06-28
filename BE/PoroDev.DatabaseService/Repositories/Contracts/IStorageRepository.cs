using PoroDev.Common.Contracts.StorageService;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository : IGenericRepository<FileData>
    {
    }
}
