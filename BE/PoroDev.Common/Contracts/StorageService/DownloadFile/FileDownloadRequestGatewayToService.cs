using MongoDB.Bson;

namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestGatewayToService
    {
        public ObjectId fileId { get; init; }
    }
}