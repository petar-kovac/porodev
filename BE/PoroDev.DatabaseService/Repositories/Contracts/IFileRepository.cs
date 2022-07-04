using MongoDB.Bson;
using PoroDev.Common.Contracts.StorageService.ReadFile;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task<ObjectId> UploadFile(string fileName, byte[] fileArray, Guid id);

        public Task<byte[]> DownloadFile(string fileId, Guid userId);

        public Task<FileReadSingleModel> ReadFiles(string fileId);
    }
}