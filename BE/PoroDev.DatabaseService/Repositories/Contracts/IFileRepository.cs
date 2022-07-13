using MongoDB.Bson;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.DatabaseService.Models;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task<ObjectId> UploadFile(string fileName, byte[] fileArray, string contentType, Guid id);

        public Task<FileReadSingleModel> ReadFiles(string fileId, string userName, string userLastName);

        public Task<FileDownloadMessage> DownloadFile(string fileId, Guid userId);

        Task<FileMetadata> ReadFileById(string fileId);
    }
}