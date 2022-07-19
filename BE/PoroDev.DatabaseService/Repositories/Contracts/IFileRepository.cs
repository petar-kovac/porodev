using MongoDB.Bson;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.DatabaseService.Models;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        Task<ObjectId> UploadFile(string fileName, byte[] fileArray, string contentType);

        Task<FileReadSingleModel> ReadFiles(string fileId, string userName, string userLastName);

        Task<FileDownload> DownloadFile(string fileId);

        Task<FileMetadata> ReadFileById(string fileId);

        Task<List<SingleFileQueryModel>> QueryFiles(SingleFileQueryServiceToDatabase queryReqeust);
    }
}