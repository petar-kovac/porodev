using PoroDev.Common.Models.StorageModels.UploadFile;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository
    {
        public Task UploadFile(byte[] fileArray, string fileName, Guid id);
    }
}
