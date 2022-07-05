

using MongoDB.Bson;
using PoroDev.Common.Contracts.StorageService.DownloadFile;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task<ObjectId> UploadFile(string fileName, byte[] fileArray, string contentType, Guid id);

        public Task<FileDownloadMessage> DownloadFile(string fileId, Guid userId);
    }
}