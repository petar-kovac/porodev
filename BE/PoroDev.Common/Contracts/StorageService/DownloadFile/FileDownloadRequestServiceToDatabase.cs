using MongoDB.Bson;

namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestServiceToDatabase
    {
        public ObjectId fileId { get; init; }
    }
}