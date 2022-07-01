

using MongoDB.Bson;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task<ObjectId> UploadFile(string fileName, byte[] fileArray, Guid id);

        public Task<byte[]> DownloadFile(string fileName);
    }
}