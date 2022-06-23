

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository
    {
        public Task UploadFile(string fileName, byte[] fileArray, Guid id);
    }
}
