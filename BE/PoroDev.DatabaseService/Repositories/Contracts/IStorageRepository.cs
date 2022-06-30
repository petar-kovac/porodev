using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Models.StorageModels.Data;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository : IGenericRepository<FileData>
    {
    }
}
