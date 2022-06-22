using PoroDev.Common.Models.StorageModels.UploadFile;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository
    {
        public Task UploadFile(Stream stream, string fileName, Guid id);
    }
}
