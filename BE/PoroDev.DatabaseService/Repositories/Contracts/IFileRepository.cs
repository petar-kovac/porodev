

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task UploadFile(string fileName, byte[] fileArray, Guid id);
    }
}
